using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.DB.Models
{
    public class DocumentFile
    {
        public int DocumentFileID { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileData { get; set; }
        public Document Document { get; set; }
        public int DocumentID { get; set; }
    }
}
