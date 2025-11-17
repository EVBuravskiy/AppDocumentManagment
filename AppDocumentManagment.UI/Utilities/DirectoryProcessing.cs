using System.Diagnostics;
using System.IO;

namespace AppDocumentManagment.UI.Utilities
{
    public class DirectoryProcessing
    {
        public static void OpenDirectory(string folderPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = folderPath
            };
            Process.Start(psi);
        }
    }
}
