namespace AppDocumentManagment.DB.Models
{
    public class ProductionSubTask
    {
        public int ProductionSubTaskID { get; set; }
        public string ProductionSubTaskDescription { get; set; }
        public DateTime ProductionSubTaskCreateTime { get; set; }
        public ProductionTask ProductionTask { get; set; }
        public int ProductionTaskID { get; set; }
    }
}
