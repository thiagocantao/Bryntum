using Cdis.Brisk.DataTransfer.Gantt;
using Cdis.Brisk.DataTransfer.Gantt.Balanceamento;
using Cdis.Brisk.Domain.Domains.Cronograma;
using Cdis.Brisk.Infra.Core.Interface;
using Cdis.Brisk.Service.Services.Cronograma;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cdis.Brisk.Application.Applications.Cronograma
{
    /// <summary>
    /// Classe de aplicação CronogramaGanttBalanceamentoApplication
    /// </summary>
    public class CronogramaGanttBalanceamentoApplication : IApplication
    {
        #region Properties

        private readonly string _strDateFormatPtBr = "dd/MM/yyyy";
        private readonly string _strDateFormatEn = "MM/dd/yyyy";
        private readonly string _strDateFormatProject = "yyyy'-'MM'-'dd";

        /// <summary>
        /// Propriedade unit of work da classe de aplicação CronogramaGanttBalanceamentoApplication
        /// </summary>
        private readonly UnitOfWorkApplication _unitOfWorkApplication;

        /// <summary>
        /// Propriedade pública da unit of work da classe de aplicação CronogramaGanttBalanceamentoApplication
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
        /// Construtor da classe de aplicação CronogramaGanttBalanceamentoApplication
        /// </summary>
        public CronogramaGanttBalanceamentoApplication(UnitOfWorkApplication unitOfWorkApplication)
        {
            _unitOfWorkApplication = unitOfWorkApplication;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Buscar as informações para alimentar o gráfico de gantt do Bryntum para balanceamento
        /// </summary>        
        public GanttDatasetDataTransfer GetGanttDatasetDataTransfer(int codEntidade, int codPortfolio, int numCenario, Type typeResourceTraducao)
        {

            var listTasks = UowApplication.UowService.GetUowService<CronogramaBalanceamentoService>().GetListCronogramaBalanceamento(codEntidade, codPortfolio, numCenario);

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
        private TasksGanttBalanceamentoDataTransfer MontarTasksGanttDataTransfer(List<CronogramaBalanceamentoDomain> listTasks, Type typeResourceTraducao)
        {
            return new TasksGanttBalanceamentoDataTransfer
            {
                rows = MontarRowsTaskDataTransfer(listTasks, typeResourceTraducao)
            };
        }

        /// <summary>
        /// 
        /// </summary>        
        private List<TaskItemGanttBalanceamentoDataTransfer> MontarRowsTaskDataTransfer(List<CronogramaBalanceamentoDomain> listTasks, Type typeResourceTraducao)
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

            return listTasks.Select(b => new TaskItemGanttBalanceamentoDataTransfer
            {
                descricao = b.Descricao,
                cor = b.Cor,
                icone = b.Cor.Trim().Equals("Verde")
                        ? string.Format(tagIcone, "green", situacaoGanntVerde)
                        : b.Cor.Trim().Equals("Vermelho")
                          ? string.Format(tagIcone, "red", situacaoGanntVermelho)
                          : b.Cor.Trim().Equals("Amarelo")
                            ? string.Format(tagIcone, "yellow", situacaoGanntAmarelo)
                            : string.Format(tagIcone, "gray", situacaoGanntCinza),
                startDate = b.Inicio.HasValue ? b.Inicio.Value.ToString(_strDateFormatProject) : "",
                endDate = b.Termino.HasValue ? b.Termino.Value.ToString(_strDateFormatProject) : "",
                constraintDate = b.Inicio.HasValue ? b.Inicio.Value.ToString(_strDateFormatProject) : "",
                constraintType = "muststarton",
                inicio = b.Inicio.HasValue ? b.Inicio.Value.ToString(strFormatDate) : "",
                termino = b.Termino.HasValue ? b.Termino.Value.ToString(strFormatDate) : "",
                percentDone = b.Concluido
            }).ToList();
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
