using AppDocumentManagment.DB.Models;
using System.Windows;

namespace AppDocumentManagment.DB.Controllers
{
    public class DocumentController
    {
        public List<Document> GetAllDocuments()
        {
            List<Document> documents = new List<Document>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documents = context.Documents.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в получении файла документа из базы данных");
            }
            return documents;
        }
        public bool AddDocument(Document document)
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
                    context.Documents.Add(document);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в сохранении документа в базу данных");
                return false;
            }
        }

        public bool RemoveDocument(Document document)
        {
            if (document == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Document aviableDocument = context.Documents.Where(x => x.DocumentID == document.DocumentID).FirstOrDefault();
                    if (aviableDocument != null)
                    {
                        context.Documents.Remove(aviableDocument);
                        context.SaveChanges();
                    }
                    List<DocumentFile> files = context.DocumentFiles.Where(d => d.DocumentID == document.DocumentID).ToList();
                    if(files != null && files.Count > 0)
                    {
                        context.DocumentFiles.RemoveRange(files);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в удалении документа из базы данных или его файлов");
                return false;
            }
        }

        public bool UpdateDocument(Document document)
        {
            bool result = false;
            if (document == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Document aviableDocument = context.Documents.Where(x => x.DocumentID == document.DocumentID).FirstOrDefault();
                    if (aviableDocument != null)
                    {
                        aviableDocument.DocumentTitle = document.DocumentTitle;
                        aviableDocument.DocumentNumber = document.DocumentNumber;
                        aviableDocument.DocumentDate = document.DocumentDate;
                        ContractorCompany contractorCompany = context.ContractorCompanies.Where(x => x.ContractorCompanyID == document.ContractorCompany.ContractorCompanyID).FirstOrDefault();
                        aviableDocument.ContractorCompany = contractorCompany;
                        aviableDocument.DocumentType = document.DocumentType;
                        aviableDocument.DocumentFiles = document.DocumentFiles;
                        aviableDocument.RegistrationDate = document.RegistrationDate;
                        aviableDocument.IsRegistated = document.IsRegistated;
                        Employee employee = context.Employees.Where(x => x.EmployeeID == document.EmployeeReceivedDocument.EmployeeID).FirstOrDefault();
                        aviableDocument.EmployeeReceivedDocument = employee;
                        aviableDocument.SendingDate = document.SendingDate;
                        context.Documents.Update(aviableDocument);
                        context.SaveChanges();
                        result = true; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в обновлении документа в базе данных или его файлов");
                result = false;
            }
            return result;
        }
    }
}
