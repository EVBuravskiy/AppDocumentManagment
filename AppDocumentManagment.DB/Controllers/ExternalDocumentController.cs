using AppDocumentManagment.DB.Models;
using System.Windows;

namespace AppDocumentManagment.DB.Controllers
{
    public class ExternalDocumentController
    {
        public List<ExternalDocument> GetAllDocuments()
        {
            List<ExternalDocument> documents = new List<ExternalDocument>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documents = context.ExternalDocuments.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в получении файла документа из базы данных");
            }
            return documents;
        }

        public ExternalDocument GetExternalDocumentByExternalDocumentID(int externalDocumentID)
        {
            ExternalDocument document = null;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    document = context.ExternalDocuments.SingleOrDefault(d => d.ExternalDocumentID == externalDocumentID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в получении документа по id из базы данных");
            }
            return document;
        }

        public List<ExternalDocument> GetExternalDocumentsByEmployeeReceivedDocumentID(int employeeReceivedDocumentID)
        {
            List<ExternalDocument> documents = new List<ExternalDocument>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documents = context.ExternalDocuments.Where(d => d.EmployeeID == employeeReceivedDocumentID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в получении файла документа из базы данных");
            }
            return documents;
        }
        public bool AddDocument(ExternalDocument document)
        {
            if (document == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ContractorCompany contractorCompany = context.ContractorCompanies.Where(x => x.ContractorCompanyID == document.ContractorCompany.ContractorCompanyID).FirstOrDefault();
                    document.ContractorCompany = contractorCompany;
                    if (document.EmployeeReceivedDocument != null)
                    {
                        Employee employee = context.Employees.Where(e => e.EmployeeID == document.EmployeeReceivedDocument.EmployeeID).FirstOrDefault();
                        document.EmployeeReceivedDocument = employee;
                    }
                    context.ExternalDocuments.Add(document);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                    Console.WriteLine("Ошибка в сохранении документа в базу данных");
                return false;
            }
        }

        public bool RemoveDocument(ExternalDocument document)
        {
            if (document == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument aviableDocument = context.ExternalDocuments.Where(x => x.ExternalDocumentID == document.ExternalDocumentID).FirstOrDefault();
                    if (aviableDocument != null)
                    {
                        context.ExternalDocuments.Remove(aviableDocument);
                        context.SaveChanges();
                    }
                    List<ExternalDocumentFile> files = context.ExternalDocumentFiles.Where(d => d.ExternalDocumentID == document.ExternalDocumentID).ToList();
                    if(files != null && files.Count > 0)
                    {
                        context.ExternalDocumentFiles.RemoveRange(files);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в удалении документа из базы данных или его файлов");
                return false;
            }
        }

        public bool UpdateDocument(ExternalDocument document)
        {
            bool result = false;
            if (document == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument aviableDocument = context.ExternalDocuments.Where(x => x.ExternalDocumentID == document.ExternalDocumentID).FirstOrDefault();
                    if (aviableDocument != null)
                    {
                        aviableDocument.ExternalDocumentTitle = document.ExternalDocumentTitle;
                        aviableDocument.ExternalDocumentNumber = document.ExternalDocumentNumber;
                        aviableDocument.ExternalDocumentDate = document.ExternalDocumentDate;
                        ContractorCompany contractorCompany = context.ContractorCompanies.Where(x => x.ContractorCompanyID == document.ContractorCompany.ContractorCompanyID).FirstOrDefault();
                        aviableDocument.ContractorCompany = contractorCompany;
                        aviableDocument.ExternalDocumentType = document.ExternalDocumentType;
                        aviableDocument.ExternalDocumentFiles = document.ExternalDocumentFiles;
                        aviableDocument.RegistrationDate = document.RegistrationDate;
                        aviableDocument.IsRegistated = document.IsRegistated;
                        if (document.EmployeeReceivedDocument != null)
                        {
                            Employee employee = context.Employees.Where(x => x.EmployeeID == document.EmployeeReceivedDocument.EmployeeID).FirstOrDefault();
                            aviableDocument.EmployeeReceivedDocument = employee;
                            aviableDocument.SendingDate = document.SendingDate;
                        }
                        aviableDocument.ExternalDocumentStatus = document.ExternalDocumentStatus;
                        context.ExternalDocuments.Update(aviableDocument);
                        context.SaveChanges();
                        result = true; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в обновлении документа в базе данных или его файлов");
                result = false;
            }
            return result;
        }
    }
}
