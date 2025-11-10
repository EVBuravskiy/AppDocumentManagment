namespace AppDocumentManagment.DB.Models
{
    public class ExtermalDocument
    {
        public int DocumentID { get; set; }
        public string DocumentTitle { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public ContractorCompany ContractorCompany { get; set; }
        public int ContractorCompanyID { get; set; }
        public ExternalDocumentType DocumentType { get; set; }
        public List<ExternalDocumentFile>? DocumentFiles { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationNumber => DocumentID.ToString();
        public bool IsRegistated { get; set; }
        public Employee? EmployeeReceivedDocument { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? SendingDate { get; set; }
        public ExternalDocumentStatus ExternalDocumentStatus { get; set; }
    }
}
