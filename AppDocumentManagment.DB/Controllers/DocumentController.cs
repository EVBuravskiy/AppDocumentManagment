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
    }
}
