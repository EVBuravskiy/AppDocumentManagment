namespace AppDocumentManagment.DB.Models
{
    public class ExternalDocument
    {
        public int ExternalDocumentID { get; set; }
        public string ExternalDocumentTitle { get; set; }
        public string? ExternalDocumentNumber { get; set; }
        public DateTime ExternalDocumentDate { get; set; }
        public ContractorCompany ContractorCompany { get; set; }
        public int ContractorCompanyID { get; set; }
        public ExternalDocumentType ExternalDocumentType { get; set; }
        public List<ExternalDocumentFile>? ExternalDocumentFiles { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationNumber => ExternalDocumentID.ToString();
        public bool IsRegistated { get; set; }
        public Employee? EmployeeReceivedDocument { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? SendingDate { get; set; }
        public DocumentStatus ExternalDocumentStatus { get; set; }
        public ProductionTask? ProductionTask { get; set; }
        public int ProductionTaskID { get; set; } = 0;
    }
}
