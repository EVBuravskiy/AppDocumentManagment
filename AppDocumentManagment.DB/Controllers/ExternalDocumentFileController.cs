using AppDocumentManagment.DB.Models;
using System.Windows;

namespace AppDocumentManagment.DB.Controllers
{
    public class ExternalDocumentFileController
    {
        public List<ExternalDocumentFile> GetDocumentFiles(int documentID)
        {
            if (documentID == 0) return null;
            List<ExternalDocumentFile> documentFiles = new List<ExternalDocumentFile>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documentFiles = context.DocumentFiles.Where(df => df.ExternalDocumentID == documentID).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в получении файла документа из базы данных");
            }
            return documentFiles;
        }

        public bool AddDocumentFiles(List<ExternalDocumentFile> documentFiles)
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

        public bool AddDocumentFile(ExternalDocumentFile documentFile, ExternalDocument externalDocument)
        {
            if (documentFile == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocument aviableExternalDocument = context.Documents.Where(d => d.ExternalDocumentID == externalDocument.ExternalDocumentID).FirstOrDefault();
                    if (aviableExternalDocument != null)
                    {
                        documentFile.ExternalDocument = aviableExternalDocument;
                        context.DocumentFiles.Add(documentFile);
                        context.SaveChanges();
                        return true;
                    }
                    MessageBox.Show("Ошибка при добавлении файла входящего документа в базу данных.\nВходящий документ не был найден");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в добавлении файла входящего документа в базу данных");
                return false;
            }
        }

        public bool RemoveDocumentFile(ExternalDocumentFile documentFile)
        {
            if (documentFile == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    ExternalDocumentFile aviableDocumentFile = context.DocumentFiles.Where(f => f.ExternalDocumentFileID == documentFile.ExternalDocumentFileID).FirstOrDefault();
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
