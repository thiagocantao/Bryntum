using System.Collections.Generic;

namespace Cdis.Brisk.DataTransfer.Gantt.PlanoAcao
{
    public class TaskItemGanttPlanoAcaoDataTransfer
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string inicio { get; set; }
        public string termino { get; set; }
        public string descricao { get; set; }
        public decimal? percentDone { get; set; }
        public List<TaskItemGanttPlanoAcaoDataTransfer> children { get; set; }
    }
}
