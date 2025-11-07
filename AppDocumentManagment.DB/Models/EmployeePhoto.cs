namespace AppDocumentManagment.DB.Models
{
    public class EmployeePhoto
    {
        public int EmployeePhotoID { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileData { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeID { get; set; }
        public string FilePath { get; set; }
    }
}
