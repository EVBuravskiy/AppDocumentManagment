namespace AppDocumentManagment.UI.Utilities
{
    public interface IFileDialogService
    {
        string OpenFile();
        string OpenFile(string fileExtension);
        void ShowMessageBox(string message);
        string SaveFile(string fileExtension);
    }
}
