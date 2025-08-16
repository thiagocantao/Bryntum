using System.Collections.Generic;
using Cdis.Brisk.DataTransfer.Gantt;

namespace Cdis.Brisk.DataTransfer.Gantt.ProjetoMeta
{
    public class TasksGanttProjetoMetaDataTransfer : TasksGanttDataTransfer
    {
        public new List<TaskItemGanttProjetoMetaDataTransfer> rows { get; set; }
    }
}

