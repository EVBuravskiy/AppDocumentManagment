using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace AppDocumentManagment.UI.ViewModels
{
    public class DocumentViewController : BaseViewModelClass
    {
        private IImageDialogService fileDialogService;
        private DocumentWindow DocumentWindow { get; set; }
        private Document SelectedDocument { get; set; }
        private string documentTitle;
        public string DocumentTitle
        {
            get => documentTitle;
            set
            {
                documentTitle = value;
                OnPropertyChanged(nameof(DocumentTitle));
            }
        }

        private string documentNumber;
        public string DocumentNumber 
        { 
            get => documentNumber; 
            set
            {
                documentNumber = value;
                OnPropertyChanged(nameof(DocumentNumber));
            }
        }

        private DateTime documentDate;
        public DateTime DocumentDate
        {
            get => documentDate;
            set
            {
                documentDate = value;
                OnPropertyChanged(nameof(documentDate));
            }
        }

        private ContractorCompany contractorCompany;
        public ContractorCompany ContractorCompany
        {
            get => contractorCompany;
            set
            {
                contractorCompany = value;
                OnPropertyChanged(nameof(ContractorCompany));
            }
        }
        private int ContractorCompanyID {get; set;}

        public List<string> DocumentTypes { get; set; }

        private DocumentType selectedDocumentType;

        public DocumentType SelectedDocumentType
        {
            get => selectedDocumentType;
            set
            {
                selectedDocumentType = value;
                OnPropertyChanged(nameof(SelectedDocumentType));
                //SelectedDocumentTypeIndex = DocumentTypeConverter.ToIntConvert(value);
            }
        }

        private int selectedDocumentTypeIndex;
        public int SelectedDocumentTypeIndex
        {
            get => selectedDocumentTypeIndex;
            set
            {
                if (selectedDocumentTypeIndex != value)
                {
                    selectedDocumentTypeIndex = value;
                    OnPropertyChanged(nameof(SelectedDocumentTypeIndex));
                    SelectedDocumentType = DocumentTypeConverter.BackConvert(value);
                }
            }
        }

        private string textBlockCompanyTitle = "Наименование контрагента";
        public string TextBlockCompanyTitle
        {
            get => textBlockCompanyTitle;
            set
            {
                textBlockCompanyTitle = value;
                OnPropertyChanged(nameof(TextBlockCompanyTitle));
            }
        }

        private string textBlockCompanyShortTitle = "Сокращенное наименование контрагента";
        public string TextBlockCompanyShortTitle
        {
            get => textBlockCompanyShortTitle;
            set
            {
                textBlockCompanyShortTitle = value;
                OnPropertyChanged(nameof(TextBlockCompanyShortTitle));
            }
        }

        private string textBlockCompanyAddress = "Юридический адрес:";
        public string TextBlockCompanyAddress
        {
            get => textBlockCompanyAddress;
            set
            {
                textBlockCompanyAddress = value;
                OnPropertyChanged(nameof(TextBlockCompanyAddress));
            }
        }

        private string textBlockCompanyPhone = "Контактный телефон:";
        public string TextBlockCompanyPhone
        {
            get => textBlockCompanyPhone;
            set
            {
                textBlockCompanyPhone = value;
                OnPropertyChanged(nameof(TextBlockCompanyPhone));
            }
        }

        private List<DocumentFile> DocumentFilesList { get; set; }
        
        public ObservableCollection<DocumentFile> DocumentFiles { get; set; }

        public DocumentFile SelectedDocumentFile {  get; set; }

        public DocumentViewController(DocumentWindow window, Document selectedDocument)
        {
            DocumentWindow = window;
            SelectedDocument = selectedDocument;
            fileDialogService = new WindowsDialogService();
            DocumentFiles = new ObservableCollection<DocumentFile>();
            if (SelectedDocument != null)
            {
                DocumentTitle = SelectedDocument.DocumentTitle;
                DocumentNumber = SelectedDocument.DocumentNumber;
                DocumentDate = SelectedDocument.DocumentDate;
                ContractorCompany = SelectedDocument.ContractorCompany;
                ContractorCompanyID = SelectedDocument.ContractorCompanyID;
                SelectedDocumentType = SelectedDocument.DocumentType;
                GetDocumentFiles();
                InitializeDocumentFiles();
            }
            else
            {
                DocumentDate = DateTime.Now;
                DocumentFilesList = new List<DocumentFile>();
                SelectedDocumentType = DocumentType.Contract;
            } 
            InitializeDocumentTypes();
        }

        private void InitializeDocumentTypes()
        {
            DocumentTypes = new List<string>();
            var documentTypes = Enum.GetValues(typeof(DocumentType));
            foreach (var type in documentTypes)
            {
                DocumentTypes.Add(DocumentTypeConverter.ConvertToString(type));
            }
        }

        public ICommand IOpenContractorCompanyListWindow => new RelayCommand(openContractorCompanyListWindow => OpenContractorCompanyListWindow());
        private void OpenContractorCompanyListWindow()
        {
            ContractorСompanyListWindow contractorСompanyListWindow = new ContractorСompanyListWindow();
            contractorСompanyListWindow.ShowDialog();
            ContractorCompany = contractorСompanyListWindow.viewModel.SelectedContractorCompany;
            OnPropertyChanged(nameof(ContractorCompany));
            TextBlockCompanyTitle = ContractorCompany.ContractorCompanyTitle;
            TextBlockCompanyShortTitle = ContractorCompany.ContractorCompanyShortTitle;
            TextBlockCompanyAddress = $"Юридический адрес: {ContractorCompany.ContractorCompanyAddress}";
            TextBlockCompanyPhone = $"Контактный телефон: {ContractorCompany.ContractorCompanyPhone}";
        }

        private void GetDocumentFiles()
        {
            if(SelectedDocument != null)
            {
                DocumentFilesList = new List<DocumentFile>();
                DocumentFileController documentFileController = new DocumentFileController();
                DocumentFilesList = documentFileController.GetDocumentFiles(SelectedDocument.DocumentID);
            }
        }

        private void InitializeDocumentFiles()
        {
            DocumentFiles.Clear();
            if (DocumentFilesList != null && DocumentFilesList.Count > 0)
            {
                foreach (var file in DocumentFilesList)
                {
                    DocumentFiles.Add(file);
                }
            }
        }

        public ICommand IBrowseDocumentFiles => new RelayCommand(browseDocumentFiles => BrowseDocumentFile());
        private void BrowseDocumentFile()
        {
            var filePath = fileDialogService.OpenFile("Files|*.txt;*.jpg;*.jpeg;*.png;*.pdf|All files");
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            DocumentFile documentFile = new DocumentFile();
            documentFile.FileName = fileName;
            documentFile.FileExtension = fileExtension;
            documentFile.FileData = fileData;
            DocumentFilesList.Add(documentFile);
            InitializeDocumentFiles();
        }

        public ICommand IDeleteDocumentFile => new RelayCommand(deleteDocumentFile => DeleteDocumentFile());
        private void DeleteDocumentFile()
        {
            bool result = false;
            if (SelectedDocument != null)
            {
                if (SelectedDocumentFile != null)
                {
                    DocumentFileController documentFileController = new DocumentFileController();
                    result = documentFileController.RemoveDocumentFile(SelectedDocumentFile);
                }
                if (result)
                {
                    MessageBox.Show("Удаление файла выполнено");
                    GetDocumentFiles();
                    InitializeDocumentFiles();
                    return;
                }
                else
                {
                    MessageBox.Show("Ошибка. Удаление файла не выполнено!");
                }
            }
            else
            {
                DocumentFilesList.Remove(SelectedDocumentFile);
                InitializeDocumentFiles();
                SelectedDocumentFile = DocumentFiles.FirstOrDefault();
            }
        }

        public ICommand IRegisterDocument => new RelayCommand(registerDocument => RegisterDocument());
        private void RegisterDocument()
        {
            Document newDocument = new Document();
            newDocument.DocumentTitle = DocumentTitle;
            newDocument.DocumentNumber = DocumentNumber;
            newDocument.DocumentDate = DocumentDate;
            newDocument.ContractorCompany = ContractorCompany;
            newDocument.ContractorCompanyID = ContractorCompanyID;
            newDocument.DocumentType = SelectedDocumentType;
            newDocument.DocumentFiles = DocumentFiles.ToList();
            if (SelectedDocumentFile == null)
            {
                newDocument.RegistrationDate = DateTime.Now;
                newDocument.IsRegistated = true;
            }
            bool result = false;
            DocumentController documentController = new DocumentController();
            result = documentController.AddDocument(newDocument);
            if (result)
            {
                MessageBox.Show("Документ зарегистрирован");
                DocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в регистрации документа");
                DocumentWindow.Close();
            }
        }
    }
}
