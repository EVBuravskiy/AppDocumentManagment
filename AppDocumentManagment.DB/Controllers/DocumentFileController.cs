using AppDocumentManagment.DB.Models;
using System.Windows;

namespace AppDocumentManagment.DB.Controllers
{
    public class DocumentFileController
    {
        public List<DocumentFile> GetDocumentFiles(int documentID)
        {
            if (documentID == 0) return null;
            List<DocumentFile> documentFiles = new List<DocumentFile>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documentFiles = context.DocumentFiles.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в получении файла документа из базы данных");
            }
            return documentFiles;
        }

        public bool AddDocumentFiles(List<DocumentFile> documentFiles)
        {
            if (documentFiles == null || documentFiles.Count == 0) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.DocumentFiles.AddRange(documentFiles);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в сохранении списка файлов документа в базу данных");
                return false;
            }
        }
        public bool RemoveDocumentFile(DocumentFile documentFile)
        {
            if (documentFile == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    DocumentFile aviableDocumentFile = context.DocumentFiles.Where(f => f.DocumentFileID == documentFile.DocumentFileID).FirstOrDefault();
                    if (aviableDocumentFile == null) return false;
                    context.DocumentFiles.Remove(aviableDocumentFile);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в удалении файла документа из базы данных");
                return false;
            }
        }
    }
}
