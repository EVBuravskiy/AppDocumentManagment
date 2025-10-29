using AppDocumentManagment.DB.Models;
using System.Windows;

namespace AppDocumentManagment.DB.Controllers
{
    public class InternalDocumentController
    {
        public List<InternalDocument> GetInternalDocuments()
        {
            List<InternalDocument> internalDocuments = new List<InternalDocument>();
            using (ApplicationContext context = new ApplicationContext())
            {
                internalDocuments = context.InternalDocuments.ToList();
            }
            return internalDocuments;
        }

        public bool AddInternalDocument(InternalDocument inputDocument)
        {
            if (inputDocument == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    if(inputDocument.Signatory == null)
                    {
                        return false;
                    }
                    else
                    {
                        Employee signatory = context.Employees.Where(e => e.EmployeeID == inputDocument.Signatory.EmployeeID).FirstOrDefault();
                        inputDocument.Signatory = signatory;
                    }
                    if (inputDocument.ApprovedManager != null)
                    {
                        Employee approvedManager = context.Employees.Where(e => e.EmployeeID == inputDocument.ApprovedManager.EmployeeID).FirstOrDefault();
                        inputDocument.ApprovedManager = approvedManager;
                    }
                    context.InternalDocuments.Add(inputDocument);
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

        public bool RemoveDocument(InternalDocument inputDocument)
        {
            if (inputDocument == null) return false;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocument aviableInternalDocument = context.InternalDocuments.Where(x => x.InternalDocumentID == inputDocument.InternalDocumentID).FirstOrDefault();
                    if (aviableInternalDocument != null)
                    {
                        context.InternalDocuments.Remove(aviableInternalDocument);
                        context.SaveChanges();
                    }
                    List<InternalDocumentFile> files = context.InternalDocumentFiles.Where(d => d.InternalDocumentID == inputDocument.InternalDocumentID).ToList();
                    if (files != null && files.Count > 0)
                    {
                        context.InternalDocumentFiles.RemoveRange(files);
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

        public bool UpdateDocument(InternalDocument inputDocument)
        {
            bool result = false;
            if (inputDocument == null) return result;
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    InternalDocument aviableDocument = context.InternalDocuments.Where(x => x.InternalDocumentID == inputDocument.InternalDocumentID).FirstOrDefault();
                    if (aviableDocument != null)
                    {
                        aviableDocument.InternalDocumentType = inputDocument.InternalDocumentType;
                        Employee signatory = context.Employees.Where(e => e.EmployeeID == inputDocument.Signatory.EmployeeID).FirstOrDefault();
                        aviableDocument.Signatory = signatory;
                        Employee approvedManager = context.Employees.Where(e => e.EmployeeID == inputDocument.ApprovedManager.EmployeeID).FirstOrDefault();
                        aviableDocument.ApprovedManager = approvedManager;
                        aviableDocument.InternalDocumentFiles = inputDocument.InternalDocumentFiles;
                        aviableDocument.RegistrationDate = inputDocument.RegistrationDate;
                        aviableDocument.IsRegistated = inputDocument.IsRegistated;
                        aviableDocument.SendingDate = inputDocument.SendingDate;
                        context.InternalDocuments.Update(aviableDocument);
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
