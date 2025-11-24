using AppDocumentManagment.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AppDocumentManagment.DB.Controllers
{
    public class InternalDocumentFileController
    {
        public List<InternalDocumentFile> GetInternalDocumentFiles(int documentID)
        {
            if (documentID == 0) return null;
            List<InternalDocumentFile> documentFiles = new List<InternalDocumentFile>();
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    documentFiles = context.InternalDocumentFiles.Where(df => df.InternalDocumentID == documentID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в получении файла документа из базы данных");
            }
            return documentFiles;
        }

        public bool AddInternalDocumentFiles(List<InternalDocumentFile> internalDocumentFiles)
        {
            if (internalDocumentFiles == null || internalDocumentFiles.Count == 0) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.InternalDocumentFiles.AddRange(internalDocumentFiles);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в сохранении списка файлов документа в базу данных");
                return false;
            }
        }

        public bool AddInternalDocumentFile(InternalDocumentFile internalDocumentFile, InternalDocument internalDocument)
        {
            if (internalDocumentFile == null) return false;
            if (internalDocument == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocument aviableInternalDocument = context.InternalDocuments.Where(d => d.InternalDocumentID == internalDocument.InternalDocumentID).FirstOrDefault();
                    if (aviableInternalDocument == null) return false;
                    internalDocumentFile.InternalDocument = aviableInternalDocument;
                    context.InternalDocumentFiles.Add(internalDocumentFile);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в сохранении файла документа в базу данных");
                return false;
            }
        }

        public bool RemoveInternalDocumentFile(InternalDocumentFile internalDocumentFile)
        {
            if (internalDocumentFile == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocumentFile aviableDocumentFile = context.InternalDocumentFiles.Where(f => f.InternalDocumentFileID == internalDocumentFile.InternalDocumentFileID).FirstOrDefault();
                    if (aviableDocumentFile == null) return false;
                    context.InternalDocumentFiles.Remove(aviableDocumentFile);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в удалении файла документа из базы данных");
                return false;
            }
        }
    }
}
