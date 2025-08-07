namespace Cdis.Brisk.DataTransfer.Gantt
{
    public class GanttDatasetDataTransfer
    {
        public bool success { get; set; }
        public ProjectGanttDataTransfer project { get; set; }
        public object tasks { get; set; }
        public DependenciesGanttDataTransfer dependencies { get; set; }
        public ResourcesGanttDataTransfer resources { get; set; }
        public AssignmentsGanttDataTransfer assignments { get; set; }
    }
}
