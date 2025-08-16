using System.Collections.Generic;
using Cdis.Brisk.DataTransfer.Gantt;

namespace Cdis.Brisk.DataTransfer.Gantt.Balanceamento
{
    public class TasksGanttBalanceamentoDataTransfer : TasksGanttDataTransfer
    {
        public new List<TaskItemGanttBalanceamentoDataTransfer> rows { get; set; }
    }
}

