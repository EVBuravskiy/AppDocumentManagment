namespace AppDocumentManagment.DB.Models
{
    public class ExternalDocumentFile
    {
        public int ExternalDocumentFileID { get; set; }
        public string ExternalFileName { get; set; }
        public string ExternalFileExtension { get; set; }
        public byte[] ExternalFileData { get; set; }
        public ExternalDocument ExternalDocument { get; set; }
        public int ExternalDocumentID { get; set; }
    }
}
