using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.UI.Utilities
{
    public interface IImageDialogService
    {
        string OpenFile(string filter);
        void ShowMessageBox(string message);
    }
}
