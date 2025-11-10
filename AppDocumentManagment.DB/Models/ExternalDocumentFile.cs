namespace AppDocumentManagment.DB.Models
{
    public class ExternalDocumentFile
    {
        public int DocumentFileID { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileData { get; set; }
        public ExtermalDocument Document { get; set; }
        public int DocumentID { get; set; }
    }
}
