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
                fileStream.Read(buffer, 0, buffer.Length);
            }
            return buffer;
        }

        public static bool SaveDocumentToFolder(ExternalDocumentFile documentFile, string filePath)
        {
            bool result = false;
            if (string.IsNullOrEmpty(filePath))
            {
                string directoryPath = $"{Directory.GetCurrentDirectory}\\ExternalDocuments\\";
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
                string directoryPath = $"{Directory.GetCurrentDirectory}\\InternalDocuments\\";
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

        public static bool SaveFileFromDB(string fileName, string directoryName, string fileExtension, byte[] fileData)
        {
            bool result = false;
            if (string.IsNullOrEmpty(fileName)) return false;
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string directoryPath = $"{downloadsPath}\\{directoryName}\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{fileName}";
            if (!File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    fileStream.Write(fileData, 0, fileData.Length);
                    result = true;
                }
            }
            return result;
        }

        public static string SaveInternalDocumentFileFromDB(InternalDocumentFile internalDocumentFile, string directoryName)
        {
            if (internalDocumentFile == null) return string.Empty;
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string directoryPath = $"{downloadsPath}\\{directoryName}\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{internalDocumentFile.FileName}";
            SaveInternalDocumentFileToPath(filePath, internalDocumentFile);
            return directoryPath;
        }

        public static bool SaveInternalDocumentFileToPath(string path, InternalDocumentFile internalDocumentFile)
        {
            bool result = false;
            if (string.IsNullOrEmpty(path)) return result;
            if (!File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    fileStream.Write(internalDocumentFile.FileData, 0, internalDocumentFile.FileData.Length);
                    result = true;
                }
            }
            return result;
        }

        public static string SaveExternalDocumentFileFromDB(ExternalDocumentFile externalDocumentFile, string directoryName)
        {
            if (externalDocumentFile == null) return string.Empty;
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string directoryPath = $"{downloadsPath}\\{directoryName}\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = $"{directoryPath}{externalDocumentFile.ExternalFileName}";
            SaveExternalDocumentFileToPath(filePath, externalDocumentFile);
            return directoryPath;
        }

        public static bool SaveExternalDocumentFileToPath(string path, ExternalDocumentFile externalDocumentFile)
        {
            bool result = false;
            if (string.IsNullOrEmpty(path)) return result;
            if (!File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    fileStream.Write(externalDocumentFile.ExternalFileData, 0, externalDocumentFile.ExternalFileData.Length);
                    result = true;
                }
            }
            return result;
        }
    }
}
