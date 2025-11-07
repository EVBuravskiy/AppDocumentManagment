using Microsoft.Win32;

namespace AppDocumentManagment.UI.Utilities
{
    public class WindowsDialogService : IFileDialogService
    {
        public string OpenFile(string filter)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }
        public string SaveFile(string path)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        public void ShowMessageBox(string message)
        {
            throw new NotImplementedException();
        }
    }
}
