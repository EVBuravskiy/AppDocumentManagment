using AppDocumentManagment.DB.Models;
using System.IO;


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

        public bool SaveFileToFolder(DocumentFile documentFile, string filePath)
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
    }
}
