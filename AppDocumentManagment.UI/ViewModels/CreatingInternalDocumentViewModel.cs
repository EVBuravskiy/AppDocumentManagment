using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class CreatingInternalDocumentViewModel : BaseViewModelClass
    {
        private CreatingInternalDocumentWindow CreatingInternalDocumentWindow { get; set; }
        private Employee currentUser;
        private IFileDialogService fileDialogService;
        private Employee CurrentSignatory;
        public List<string> InternalDocumentTypes { get; set; }

        private InternalDocumentType selectedInternalDocumentType;

        public InternalDocumentType SelectedInternalDocumentType
        {
            get => selectedInternalDocumentType;
            set
            {
                selectedInternalDocumentType = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentType));
            }
        }

        private int selectedInternalDocumentTypeIndex;
        public int SelectedInternalDocumentTypeIndex
        {
            get => selectedInternalDocumentTypeIndex;
            set
            {
                if (selectedInternalDocumentTypeIndex != value)
                {
                    selectedInternalDocumentTypeIndex = value;
                    OnPropertyChanged(nameof(SelectedInternalDocumentTypeIndex));
                    SelectedInternalDocumentType = InternalDocumentTypeConverter.BackConvert(value);
                }
            }
        }
        private DateTime internalDocumentDate;
        public DateTime InternalDocumentDate
        {
            get => internalDocumentDate;
            set
            {
                internalDocumentDate = value;
                OnPropertyChanged(nameof(InternalDocumentDate));
            }
        }

        private string internalDocumentTitle;
        public string InternalDocumentTitle
        {
            get => internalDocumentTitle;
            set
            {
                internalDocumentTitle = value;
                OnPropertyChanged(nameof(InternalDocumentTitle));
            }
        }

        private string internalDocumentContent;
        public string InternalDocumentContent
        {
            get => internalDocumentContent;
            set
            {
                internalDocumentContent = value;
                OnPropertyChanged(nameof(InternalDocumentContent));
            }
        }

        public ObservableCollection<InternalDocumentFile> InternalDocumentFiles { get; set; }

        public InternalDocumentFile SelectedInternalDocumentFile { get; set; }


        public CreatingInternalDocumentViewModel(CreatingInternalDocumentWindow window, int currentUserID)
        {
            CreatingInternalDocumentWindow = window;
            InitializeCurrentUser(currentUserID);
            CurrentSignatory = null;
            fileDialogService = new WindowsDialogService();
            InternalDocumentTypes = new List<string>();
            InitializeInternalDocumentTypes();
            InternalDocumentDate = DateTime.Now;
            InternalDocumentFiles = new ObservableCollection<InternalDocumentFile>();
        }

        private void InitializeCurrentUser(int currentUserID)
        {
            if (currentUserID == 0) return;
            EmployeeController employeeController = new EmployeeController();
            currentUser = employeeController.GetEmployeeByID(currentUserID);
        }

        private void InitializeInternalDocumentTypes()
        {
            var internalDocumentTypes = Enum.GetValues(typeof(InternalDocumentType));
            foreach (var type in internalDocumentTypes)
            {
                InternalDocumentTypes.Add(InternalDocumentTypeConverter.ConvertToString(type));
            }
            SelectedInternalDocumentType = InternalDocumentTypeConverter.ConvertToEnum(InternalDocumentTypes.FirstOrDefault());
            SelectedInternalDocumentTypeIndex = 0;
        }

        public ICommand IBrowseInternalDocumentFiles => new RelayCommand(browseInternalDocumentFiles => BrowseInternalDocumentFile());
        private void BrowseInternalDocumentFile()
        {
            var filePath = fileDialogService.OpenFile("Files|*.txt;*.jpg;*.jpeg;*.png;*.pdf|All files");
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            InternalDocumentFile internalDocumentFile = new InternalDocumentFile();
            internalDocumentFile.FileName = fileName;
            internalDocumentFile.FileExtension = fileExtension;
            internalDocumentFile.FileData = fileData;
            InternalDocumentFiles.Add(internalDocumentFile);
        }

        public ICommand IDeleteInternalDocumentFile => new RelayCommand(deleteInternalDocumentFile => DeleteInternalDocumentFile());
        private void DeleteInternalDocumentFile()
        {
            if (SelectedInternalDocumentFile != null)
            {
                InternalDocumentFiles.Remove(SelectedInternalDocumentFile);
            }
        }

        public ICommand ISendInternalDocument => new RelayCommand(sendInternalDocument => SendInternalDocument());

        private void SendInternalDocument()
        {
            CreateInternalDocument();
        }

        private void CreateInternalDocument()
        {
            InternalDocument newInternalDocument = new InternalDocument();
            newInternalDocument.InternalDocumentTitle = InternalDocumentTitle;
            newInternalDocument.InternalDocumentContent = InternalDocumentContent;
            newInternalDocument.InternalDocumentDate = InternalDocumentDate;
            newInternalDocument.InternalDocumentFiles = InternalDocumentFiles.ToList();
            newInternalDocument.InternalDocumentType = SelectedInternalDocumentType;
            //newInternalDocument.InternalDocumentStatus = DocumentStatus.UnderConsideration;
            newInternalDocument.Signatory = currentUser;
        }
    }
}
