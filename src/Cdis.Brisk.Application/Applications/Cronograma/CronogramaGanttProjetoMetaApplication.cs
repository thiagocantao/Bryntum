using Cdis.Brisk.DataTransfer.Gantt;
using Cdis.Brisk.DataTransfer.Gantt.ProjetoMeta;
using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.Cronograma;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Application.Applications.Cronograma
{
    /// <summary>
    /// Classe de aplicação CronogramaProjetoMetaApplication
    /// </summary>
    public class CronogramaGanttProjetoMetaApplication : IApplication
    {
        #region Properties
        private readonly string _strDateFormatPtBr = "dd/MM/yyyy";
        private readonly string _strDateFormatEn = "MM/dd/yyyy";
        private readonly string _strDateFormatProject = "yyyy'-'MM'-'dd";

        /// <summary>
        /// Propriedade unit of work da classe de aplicação CronogramaProjetoMetaApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação CronogramaProjetoMetaApplication
        /// </summary>
        public UnitOfWorkApplication UowApplication
        {
            get
            {
                return _unitOfWorkApplication;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor da classe de aplicação CronogramaProjetoMetaApplication
        /// </summary>
        public CronogramaGanttProjetoMetaApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Buscar as informações para alimentar o gráfico de gantt do Bryntum para o plano de ação
        /// </summary>        
        public GanttDatasetDataTransfer GetGanttProjetoMetaDatasetDataTransfer(int idEntidade, int idUsuario, int idCarteira, Type typeResourceTraducao)
        {
            var listTasks = UowApplication.UowService.GetUowService<CronogramaProjetoMetaService>().GetListCronogramaProjetoMeta(idEntidade, idUsuario, idCarteira);

            DateTime dtStart = listTasks.Any() ? listTasks.Where(d => d.Inicio.HasValue).Min(d => d.Inicio.Value) : DateTime.Now;
            DateTime dtEnd = listTasks.Any() ? listTasks.Where(d => d.Termino.HasValue).Max(d => d.Termino.Value) : DateTime.Now;
            GanttDatasetDataTransfer ganttDataset = new GanttDatasetDataTransfer
            {
                success = true,
                project = MontarProjectDataTransfer("general", dtStart, dtEnd),
                tasks = MontarTasksGanttDataTransfer(listTasks, typeResourceTraducao),
            };
            return ganttDataset;
        }

        /// <summary>
        /// Montar a lista de tasks
        /// </summary>        
        private TasksGanttProjetoMetaDataTransfer MontarTasksGanttDataTransfer(List<CronogramaProjetoMetaDomain> listTasks, Type typeResourceTraducao)
        {
            return new TasksGanttProjetoMetaDataTransfer
            {
                rows = MontarRowsTaskDataTransfer(listTasks, typeResourceTraducao)
            };
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<TaskItemGanttProjetoMetaDataTransfer> MontarRowsTaskDataTransfer(List<CronogramaProjetoMetaDomain> listTasks, Type typeResourceTraducao)
        {
            List<TaskItemGanttProjetoMetaDataTransfer> listTaskGantt = new List<TaskItemGanttProjetoMetaDataTransfer>();

            foreach (var task in listTasks.Where(g => g.CodigoSuperior == null))
            {
                listTaskGantt.Add(MontarTaskItemGanttProjetoMetaDataTransfer(task, listTasks, typeResourceTraducao));
            }

            return listTaskGantt;
        }

        /// <summary>
        /// Montar TaskGanttDataTransfer
        /// </summary>        
        private TaskItemGanttProjetoMetaDataTransfer MontarTaskItemGanttProjetoMetaDataTransfer(CronogramaProjetoMetaDomain task, List<CronogramaProjetoMetaDomain> listTasks, Type typeResourceTraducao)
        {
            string strFormatDate = System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Contains("pt") ? _strDateFormatPtBr : _strDateFormatEn;
            string tagIcone = "<span style='color:{0};' title='{1}'><i class='b-fa b-fa-circle'></i></span>";

            var listResourceTraducao = Cdis.Brisk.Infra.Core.Util.ResourceUtil.GetListResourceItem(typeResourceTraducao, new List<string>
                {
                    "satisfat_rio",
                    "cr_tico",
                    "sem_informa__o",
                    "aten__o"
                });

            string situacaoGanntVerde = listResourceTraducao.FirstOrDefault(t => t.Key == "satisfat_rio").Text;
            string situacaoGanntVermelho = listResourceTraducao.FirstOrDefault(t => t.Key == "cr_tico").Text;
            string situacaoGanntCinza = listResourceTraducao.FirstOrDefault(t => t.Key == "sem_informa__o").Text;
            string situacaoGanntAmarelo = listResourceTraducao.FirstOrDefault(t => t.Key == "aten__o").Text;

            return new TaskItemGanttProjetoMetaDataTransfer
            {
                descricao = task.Descricao,
                startDate = task.Inicio.HasValue ? task.Inicio.Value.ToString(_strDateFormatProject) : "",
                endDate = task.Termino.HasValue ? task.Termino.Value.ToString(_strDateFormatProject) : "",
                constraintDate = task.Inicio.HasValue ? task.Inicio.Value.ToString(_strDateFormatProject) : "",
                constraintType = "muststarton",
                inicio = task.Inicio.HasValue ? task.Inicio.Value.ToString(strFormatDate) : "",
                termino = task.Termino.HasValue ? task.Termino.Value.ToString(strFormatDate) : "",
                percentDone = task.Concluido,
                cor = task.Cor,
                icone = task.Cor.Trim().Equals("Verde")
                        ? string.Format(tagIcone, "green", situacaoGanntVerde)
                        : task.Cor.Trim().Equals("Vermelho")
                          ? string.Format(tagIcone, "red", situacaoGanntVermelho)
                          : task.Cor.Trim().Equals("Cinza")
                            ? string.Format(tagIcone, "gray", situacaoGanntCinza)
                            : string.Format(tagIcone, "white", situacaoGanntCinza),
                children = GetListTaskItemGanttPlanoAcaoDataTransfer(task, listTasks, typeResourceTraducao),
                gerente = task.GerenteProjeto,
                situacao = task.StatusProjeto
            };

        }

        /// <summary>
        /// Get ListTaskGanttDataTransfer
        /// </summary>        
        private List<TaskItemGanttProjetoMetaDataTransfer> GetListTaskItemGanttPlanoAcaoDataTransfer(CronogramaProjetoMetaDomain task, List<CronogramaProjetoMetaDomain> listTasks, Type typeResourceTraducao)
        {
            if (listTasks.Any(t => t.CodigoSuperior == task.Codigo))
            {
                List<TaskItemGanttProjetoMetaDataTransfer> listTaskGantt = new List<TaskItemGanttProjetoMetaDataTransfer>();

                foreach (var taskItem in listTasks.Where(g => g.CodigoSuperior == task.Codigo))
                {
                    listTaskGantt.Add((MontarTaskItemGanttProjetoMetaDataTransfer(taskItem, listTasks, typeResourceTraducao)));
                }

                return listTaskGantt;
            }
            else
            {
                return new List<TaskItemGanttProjetoMetaDataTransfer>();
            }
        }

        /// <summary>
        /// Montar as informações do projeto
        /// </summary>        
        private ProjectGanttDataTransfer MontarProjectDataTransfer(string nomeCalendar, DateTime dtStartDate, DateTime dtEndDate)
        {
            return new ProjectGanttDataTransfer
            {
                calendar = nomeCalendar,
                startDate = dtStartDate.ToString(_strDateFormatProject),
                endDate = dtEndDate.ToString(_strDateFormatProject)
            };
        }

        #endregion
    }
}
