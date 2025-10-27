using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.DB.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string DocumentTitle { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public ContractorCompany ContractorCompany { get; set; }
        public int ContractorCompanyID { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<DocumentFile>? DocumentFiles { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RegistrationNumber => DocumentID.ToString();
        public bool IsRegistated { get; set; }
    }
}
