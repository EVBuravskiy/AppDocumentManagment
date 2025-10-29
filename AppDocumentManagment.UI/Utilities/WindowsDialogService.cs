using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ShowMessageBox(string message)
        {
            throw new NotImplementedException();
        }
    }
}
