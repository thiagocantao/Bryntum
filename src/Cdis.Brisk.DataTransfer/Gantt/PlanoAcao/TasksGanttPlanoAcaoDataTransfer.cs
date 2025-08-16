using System.Collections.Generic;
using Cdis.Brisk.DataTransfer.Gantt;

namespace Cdis.Brisk.DataTransfer.Gantt.PlanoAcao
{
    public class TasksGanttPlanoAcaoDataTransfer : TasksGanttDataTransfer
    {
        public new List<TaskItemGanttPlanoAcaoDataTransfer> rows { get; set; }
    }
}

