namespace AppDocumentManagment.DB.Models
{
    public class InternalDocument
    {
        public int InternalDocumentID { get; set; }
        public InternalDocumentTypes InternalDocumentType { get; set; }
        public DateTime InternalDocumentDate { get; set; }
        public List<Employee> Employees { get; set; }
        public Employee? Signatory;
        public int SignatoryID { get; set; }
        public Employee? ApprovedManager;
        public int ApprovedManagerID { get; set; }
        public Employee? EmployeeRecivedDocument;
        public int EmployeeRecivedDocumentID { get; set; }
        public List<InternalDocumentFile>? InternalDocumentFiles { get; set; }  
        public DateTime RegistrationDate { get; set; }
        public bool IsRegistated { get; set; }
        public DateTime? SendingDate { get; set; }
        public InternalDocumentStatus InternalDocumentStatus { get; set; }
    }
}
