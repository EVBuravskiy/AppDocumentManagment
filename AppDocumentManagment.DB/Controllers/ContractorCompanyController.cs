using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.DB.Controllers
{
    public class ContractorCompanyController
    {
        public bool AddContractorCompany(ContractorCompany contractorCompany)
        {
            if (contractorCompany == null) return false;
            using (ApplicationContext context = new ApplicationContext())
            {
                context.ContractorCompanies.Add(contractorCompany);
                context.SaveChanges();
                return true;
            }
        }
        public List<ContractorCompany> GetContractorCompanies()
        {
            List<ContractorCompany> contractorCompanies = new List<ContractorCompany>();
            using (ApplicationContext context = new ApplicationContext())
            {
                contractorCompanies = context.ContractorCompanies.ToList();
            }
            return contractorCompanies;
        }
        public ContractorCompany GetContractorCompany(ContractorCompany contractorCompany)
        {
            if (contractorCompany != null)
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ContractorCompany aviableContractorCompany = context.ContractorCompanies.Where(x => x.ContractorCompanyID == contractorCompany.ContractorCompanyID).FirstOrDefault();
                    if (aviableContractorCompany != null)
                    {
                        return aviableContractorCompany;
                    }
                }
            }
            return null;
        }
        public bool RemoveContractorCompany(ContractorCompany contractorCompany)
        {
            bool result = false;
            if (contractorCompany != null)
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ContractorCompany aviableContractorCompany = context.ContractorCompanies.Where(x => x.ContractorCompanyID == contractorCompany.ContractorCompanyID).FirstOrDefault();
                    if (aviableContractorCompany != null)
                    {
                        context.ContractorCompanies.Remove(aviableContractorCompany);
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
