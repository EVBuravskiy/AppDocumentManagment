using AppDocumentManagment.DB.Models;
using System.IO;
using System.Windows;


namespace AppDocumentManagment.UI.Utilities
{
    public class FileProcessing
    {
        public static string GetFileName(string filePath)
        {
            int lastIndex = filePath.LastIndexOf('\\');
            return filePath.Substring(lastIndex + 1);
        }

        public static string GetFileExtension(string filePath)
        {
            int lastIndex = filePath.LastIndexOf(".");
            return filePath.Substring(lastIndex + 1);
        }

        public static byte[] GetFileData(string filePath)
        {
            byte[] buffer = new byte[0];
            if (File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    buffer = new byte[fileStream.Length];
                    fileStream.ReadAsync(buffer, 0, buffer.Length);
                }
            }
            return buffer;
        }

        public static bool SaveDocumentToFolder(DocumentFile documentFile, string filePath)
        {
            bool result = false;
            if (string.IsNullOrEmpty(filePath)) 
            {
                string directoryPath = $"{Directory.GetCurrentDirectory}\\Documents\\";
                filePath = $"{directoryPath}{documentFile.FileName}";
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fileStream.Write(documentFile.FileData, 0, documentFile.FileData.Length);
                result = true;
            }
            return result;
        }

        public static bool SaveInternalDocumentToFolder(InternalDocumentFile internalDocumentFile, string filePath)
        {
            bool result = false;
            if (string.IsNullOrEmpty(filePath))
            {
                string directoryPath = $"{Directory.GetCurrentDirectory}\\Documents\\";
                filePath = $"{directoryPath}{internalDocumentFile.FileName}";
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fileStream.Write(internalDocumentFile.FileData, 0, internalDocumentFile.FileData.Length);
                result = true;
            }
            return result;
        }
        public static string SaveEmployeePhotoToTempFolder(EmployeePhoto photo, bool IsNewPhoto)
        {
            if (photo == null)
            {
                MessageBox.Show("Ошибка! Не удалось сохранить файл");
                return null;
            }
            string directoryPath = $"{Directory.GetCurrentDirectory}\\TempEmployeePhotos\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (IsNewPhoto)
            {
                string filePath = $"{directoryPath}{photo.FileName}";
                if (!File.Exists(filePath))
                {
                    try
                    {
                        using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            fileStream.Write(photo.FileData, 0, photo.FileData.Length);
                        }
                        return filePath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Не удалось сохранить файл");
                        return null;
                    }
                }
                return null;
            }
            else 
            {
                if (!File.Exists(photo.FilePath))
                {
                    try
                    {
                        using (FileStream fileStream = new FileStream(photo.FilePath, FileMode.OpenOrCreate))
                        {
                            fileStream.Write(photo.FileData, 0, photo.FileData.Length);
                        }
                        return photo.FilePath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Файл не был найден. Не удалось его сохранить повторно");
                        return null;
                    }
                }
                return null;
            }
        }
    }
}
