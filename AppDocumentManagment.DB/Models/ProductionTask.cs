using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocumentManagment.DB.Models
{
    public class ProductionTask
    {
        public int ProductionTaskID { get; set; }
        public string ProductionTaskTitle { get; set; }
        public bool Priority { get; set; }
        [NotMapped]
        public ExternalDocument ExternalDocument { get; set; }
        public int ExternalDocumentID { get; set; } = 0;
        [NotMapped]
        public InternalDocument InternalDocument { get; set; }
        public int InternalDocumentID { get; set; } = 0;
        public DateTime ProductionTaskCreateDate { get; set; }
        public DateTime ProductionTaskDueDate { get; set; }
        public string? ProductionTaskDescription { get; set; }
        [NotMapped]
        public List<Employee> Employees { get; set; }
        public List<int> EmployeesID { get; set; } = new List<int>();
        [NotMapped]
        public Employee EmployeeCreator { get; set; }
        public int EmployeeCreatorID { get; set; }
        public ProductionTaskStatus ProductionTaskStatus { get; set; }
        public List<ProductionSubTask>? ProductionSubTasks { get; set; }
        public List<ProductionTaskComment>? ProductionTaskComments { get; set; }
        public List<ProductionTaskFile>? ProductionTaskFiles { get; set; }
    }
}
