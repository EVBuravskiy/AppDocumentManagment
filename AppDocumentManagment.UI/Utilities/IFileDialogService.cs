namespace AppDocumentManagment.UI.Utilities
{
    public interface IFileDialogService
    {
        string OpenFile(string filter);
        void ShowMessageBox(string message);
        string SaveFile(string filePath);
    }
}
