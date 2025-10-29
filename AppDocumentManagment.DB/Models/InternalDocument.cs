namespace AppDocumentManagment.DB.Models
{
    public class InternalDocument
    {
        public int InternalDocumentID { get; set; }
        public InternalDocumentTypes InternalDocumentType { get; set; }
        public Employee Signatory {  get; set; }
        public int SignatoryID { get; set; }
        public Employee? ApprovedManager { get; set; }
        public int? ApprovedManagerID { get; set; }
        public List<InternalDocumentFile>? InternalDocumentFiles { get; set; }  
        public DateTime RegistrationDate { get; set; }
        public bool IsRegistated { get; set; }
        public DateTime? SendingDate { get; set; }
    }
}
