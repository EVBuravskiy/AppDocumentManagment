namespace AppDocumentManagment.DB.Models
{
    public class ProductionTask
    {
        public int ProductionTaskID { get; set; }
        public string ProductionTaskTitle { get; set; }
        public bool Priority { get; set; }
        public ExternalDocument? ExternalDocument { get; set; }
        public int ExternalDocumentID { get; set; }
        public InternalDocument? InternalDocument { get; set; }
        public int InternalDocumentID { get; set; }
        public DateTime ProductionTaskCreateDate { get; set; }
        public DateTime ProductionTaskDueDate { get; set; }
        public string? ProductionTaskDescription { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public ProductionTaskStatus ProductionTaskStatus { get; set; }
        public List<ProductionSubTask>? ProductionSubTasks { get; set; }
        public List<ProductionTaskComment>? ProductionTaskComments { get; set; }
    }
}
