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
    }
}
