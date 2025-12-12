using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.DB.Models
{
    public class ContractorCompany
    {
        public int ContractorCompanyID { get; set; }
        public string ContractorCompanyTitle { get; set; }
        public string? ContractorCompanyShortTitle { get; set; }
        public string ContractorCompanyAddress { get; set; }
        public string? ContractorCompanyPhone { get; set; }
        public string? ContractorCompanyEmail { get; set; }
        List<ExternalDocument> Documents { get; set; } = new List<ExternalDocument>();
        public string? ContractorCompanyInformation {  get; set; }
    }
}
