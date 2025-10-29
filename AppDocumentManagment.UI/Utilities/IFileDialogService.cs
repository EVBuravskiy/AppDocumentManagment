using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.UI.Utilities
{
    public interface IFileDialogService
    {
        string OpenFile(string filter);
        void ShowMessageBox(string message);
    }
}
