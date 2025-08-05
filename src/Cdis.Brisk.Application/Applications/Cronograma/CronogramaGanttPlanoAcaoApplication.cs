using Cdis.Brisk.DataTransfer.Gantt;
using Cdis.Brisk.DataTransfer.Gantt.PlanoAcao;
using Cdis.Brisk.Domain.Domains.DataBaseObject.Function;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.DataBaseObject.Function;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Application.Applications.Cronograma
{
    /// <summary>
    /// Classe de aplicação CronogramaGanttPlanoAcaoApplication
    /// </summary>
    public class CronogramaGanttPlanoAcaoApplication : IApplication
    {
        #region Properties

        private readonly string _strDateFormatPtBr = "dd/MM/yyyy";
        private readonly string _strDateFormatEn = "MM/dd/yyyy";
        private readonly string _strDateFormatProject = "yyyy'-'MM'-'dd";

        /// <summary>
        /// Propriedade unit of work da classe de aplicação CronogramaGanttPlanoAcaoApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação CronogramaGanttPlanoAcaoApplication
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
        /// Construtor da classe de aplicação CronogramaGanttPlanoAcaoApplication
        /// </summary>
        public CronogramaGanttPlanoAcaoApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Buscar as informações para alimentar o gráfico de gantt do Bryntum para o plano de ação
        /// </summary>        
        public GanttDatasetDataTransfer GetGanttPlanoAcaoDatasetDataTransfer(int idPlanoAcao, string iniciaisObjeto, int codObjeto, int codEntidade, bool isIncludeIniciativas)
        {

            var listTasks = isIncludeIniciativas
                            ? UowApplication.UowService.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcaoIniciativa(idPlanoAcao, iniciaisObjeto, codObjeto, codEntidade)
                            : UowApplication.UowService.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcao(idPlanoAcao, iniciaisObjeto, codObjeto, codEntidade);

            DateTime dtStart = listTasks.Any() ? listTasks.Where(d => d.Inicio.HasValue).Min(d => d.Inicio.Value) : DateTime.Now;
            DateTime dtEnd = listTasks.Any() ? listTasks.Where(d => d.Termino.HasValue).Max(d => d.Termino.Value) : DateTime.Now;
            GanttDatasetDataTransfer ganttDataset = new GanttDatasetDataTransfer
            {
                success = true,
                project = MontarProjectDataTransfer("general", dtStart, dtEnd),
                tasks = MontarTasksGanttDataTransfer(listTasks),
            };
            return ganttDataset;
        }

        /// <summary>
        /// Buscar as informações para alimentar o gráfico de gantt do Bryntum para o plano de ação
        /// </summary>        
        public GanttDatasetDataTransfer GetGanttDatasetDataTransfer(string iniciaisObjeto, int codObjeto, int codEntidade, bool isIncludeIniciativas)
        {

            var listTasks = isIncludeIniciativas
                            ? UowApplication.UowService.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcaoIniciativaEntidade(iniciaisObjeto, codObjeto, codEntidade)
                            : UowApplication.UowService.GetUowService<FGetCronogramaPAService>().GetListCronogramaPlanoAcaoEntidade(iniciaisObjeto, codObjeto, codEntidade);

            DateTime dtStart = listTasks.Any() ? listTasks.Where(d => d.Inicio.HasValue).Min(d => d.Inicio.Value) : DateTime.Now;
            DateTime dtEnd = listTasks.Any() ? listTasks.Where(d => d.Termino.HasValue).Max(d => d.Termino.Value) : DateTime.Now;
            GanttDatasetDataTransfer ganttDataset = new GanttDatasetDataTransfer
            {
                success = true,
                project = MontarProjectDataTransfer("general", dtStart, dtEnd),
                tasks = MontarTasksGanttDataTransfer(listTasks),
            };
            return ganttDataset;
        }

        /// <summary>
        /// Montar a lista de tasks
        /// </summary>        
        private TasksGanttPlanoAcaoDataTransfer MontarTasksGanttDataTransfer(List<FGetCronogramaPADomain> listTasks)
        {
            return new TasksGanttPlanoAcaoDataTransfer
            {
                rows = MontarRowsTaskDataTransfer(listTasks)
            };
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<TaskItemGanttPlanoAcaoDataTransfer> MontarRowsTaskDataTransfer(List<FGetCronogramaPADomain> listTasks)
        {
            List<TaskItemGanttPlanoAcaoDataTransfer> listTaskGantt = new List<TaskItemGanttPlanoAcaoDataTransfer>();

            foreach (var task in listTasks.Where(g => string.IsNullOrEmpty(g.TarefaSuperior)))
            {
                listTaskGantt.Add(MontarTaskItemGanttPlanoAcaoDataTransfer(task, listTasks));
            }

            return listTaskGantt;
        }


        /// <summary>
        /// Montar TaskGanttDataTransfer
        /// </summary>        
        private TaskItemGanttPlanoAcaoDataTransfer MontarTaskItemGanttPlanoAcaoDataTransfer(FGetCronogramaPADomain task, List<FGetCronogramaPADomain> listTasks)
        {
            string strFormatDate = System.Threading.Thread.CurrentThread.CurrentCulture.ToString().Contains("pt") ? _strDateFormatPtBr : _strDateFormatEn;
            return new TaskItemGanttPlanoAcaoDataTransfer
            {
                descricao = task.Descricao,
                startDate = task.Inicio.HasValue ? task.Inicio.Value.ToString(_strDateFormatProject) : "",
                endDate = task.Termino.HasValue ? task.Termino.Value.ToString(_strDateFormatProject) : "",
                inicio = task.Inicio.HasValue ? task.Inicio.Value.ToString(strFormatDate) : "",
                termino = task.Termino.HasValue ? task.Termino.Value.ToString(strFormatDate) : "",
                percentDone = task.PercentualConcluido,
                children = GetListTaskItemGanttPlanoAcaoDataTransfer(task, listTasks)
            };

        }

        /// <summary>
        /// Get ListTaskGanttDataTransfer
        /// </summary>        
        private List<TaskItemGanttPlanoAcaoDataTransfer> GetListTaskItemGanttPlanoAcaoDataTransfer(FGetCronogramaPADomain task, List<FGetCronogramaPADomain> listTasks)
        {
            if (listTasks.Any(t => t.TarefaSuperior == task.EstruturaHierarquica))
            {
                List<TaskItemGanttPlanoAcaoDataTransfer> listTaskGantt = new List<TaskItemGanttPlanoAcaoDataTransfer>();

                foreach (var taskItem in listTasks.Where(g => g.TarefaSuperior == task.EstruturaHierarquica))
                {
                    listTaskGantt.Add((MontarTaskItemGanttPlanoAcaoDataTransfer(taskItem, listTasks)));
                }

                return listTaskGantt;
            }
            else
            {
                return new List<TaskItemGanttPlanoAcaoDataTransfer>();
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
