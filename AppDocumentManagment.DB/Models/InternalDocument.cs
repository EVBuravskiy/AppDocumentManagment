using System.ComponentModel.DataAnnotations.Schema;

namespace AppDocumentManagment.DB.Models
{
    public class InternalDocument
    {
        public int InternalDocumentID { get; set; }
        public InternalDocumentTypes InternalDocumentType { get; set; }
        public DateTime InternalDocumentDate { get; set; }
        public List<Employee> Employees { get; set; }
        [NotMapped]
        public Employee? Signatory {  get; set; }
        public int SignatoryID { get; set; }
        [NotMapped]
        public Employee? ApprovedManager { get; set; }
        public int ApprovedManagerID { get; set; }
        [NotMapped]
        public Employee? EmployeeRecievedDocument { get; set; }
        public int EmployeeRecievedDocumentID { get; set; }
        public List<InternalDocumentFile>? InternalDocumentFiles { get; set; }  
        public DateTime RegistrationDate { get; set; }
        public bool IsRegistated { get; set; }
        public DateTime? SendingDate { get; set; }
        public InternalDocumentStatus InternalDocumentStatus { get; set; }
        public string InternalDocumentTitle { get; set; }
        public string InternalDocumentContent { get; set; }
    }
}
