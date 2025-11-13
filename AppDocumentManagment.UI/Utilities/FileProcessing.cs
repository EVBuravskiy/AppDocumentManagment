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
            if (!File.Exists(filePath))
            {
                return buffer;
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                buffer = new byte[fileStream.Length];
                fileStream.ReadAsync(buffer, 0, buffer.Length);
            }
            return buffer;
        }

        public static bool SaveDocumentToFolder(ExternalDocumentFile documentFile, string filePath)
        {
            bool result = false;
            if (string.IsNullOrEmpty(filePath))
            {
                string directoryPath = $"{Directory.GetCurrentDirectory}\\Documents\\";
                filePath = $"{directoryPath}{documentFile.ExternalFileName}";
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fileStream.Write(documentFile.ExternalFileData, 0, documentFile.ExternalFileData.Length);
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
        public static string SaveEmployeePhotoToTempFolder(EmployeePhoto photo)
        {
            if (photo == null)
            {
                MessageBox.Show("Ошибка! Не удалось сохранить файл");
                return null;
            }
            string directoryPath = $"{Directory.GetCurrentDirectory()}\\TempEmployeePhotos\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{photo.FileName}";
            if(File.Exists(filePath))
            {
                return filePath;
            }
            else
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
        }

        public static string CopyFileToTempFolder(string sourcePath)
        {
            string directoryPath = $"{Directory.GetCurrentDirectory()}\\TempEmployeePhotos\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string fileName = GetFileName(sourcePath);
            string destPath = $"{directoryPath}{fileName}";
            if (File.Exists(sourcePath) && !File.Exists(destPath))
            {
                File.Copy(sourcePath, destPath);
            }
            return destPath;
        }

        public static bool CheckFileExist(string fileName)
        {
            string directoryPath = $"{Directory.GetCurrentDirectory()}\\TempEmployeePhotos\\";
            if (!Directory.Exists(directoryPath))
            {
                return true;
            }
            string sourcePath = $"{directoryPath}{fileName}";
            if (File.Exists(sourcePath))
            {
                return true;
            }
            return false;
        }
    }
}
