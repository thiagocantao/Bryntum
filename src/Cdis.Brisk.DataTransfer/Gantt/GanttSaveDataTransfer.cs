using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cdis.Brisk.DataTransfer.Gantt
{
    public class GanttSaveDataTransfer
    {
        public List<TaskGanttDataTransfer> ListAdd { get; set; } = new List<TaskGanttDataTransfer>();
        public List<TaskGanttDataTransfer> ListMod { get; set; } = new List<TaskGanttDataTransfer>();
        public List<TaskGanttDataTransfer> ListRem { get; set; } = new List<TaskGanttDataTransfer>();
        public List<TaskGanttDataTransfer> ListPredecessor { get; set; } = new List<TaskGanttDataTransfer>();
    }
}
