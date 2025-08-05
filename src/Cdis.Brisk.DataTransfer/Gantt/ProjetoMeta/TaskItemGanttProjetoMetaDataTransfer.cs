using System.Collections.Generic;

namespace Cdis.Brisk.DataTransfer.Gantt.ProjetoMeta
{
    public class TaskItemGanttProjetoMetaDataTransfer
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string constraintDate { get; set; }
        public string constraintType { get; set; }
        public string inicio { get; set; }
        public string termino { get; set; }
        public string descricao { get; set; }
        public decimal? percentDone { get; set; }
        public string cor { get; set; }
        public string icone { get; set; }
        public string gerente { get; set; }
        public string situacao { get; set; }
        public List<TaskItemGanttProjetoMetaDataTransfer> children { get; set; }
    }
}
