using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagment.UI.ViewModels
{
    public class DocumentViewModel : BaseViewModelClass
    {
        private IFileDialogService fileDialogService;
        private DocumentWindow DocumentWindow { get; set; }
        private ExternalDocument SelectedDocument { get; set; }
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

        private ExternalDocumentType selectedDocumentType;

        public ExternalDocumentType SelectedDocumentType
        {
            get => selectedDocumentType;
            set
            {
                selectedDocumentType = value;
                OnPropertyChanged(nameof(SelectedDocumentType));
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
                    SelectedDocumentType = ExternalDocumentTypeConverter.BackConvert(value);
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

        private string textBlockCompanyEmail = "Электронная почта:";
        public string TextBlockCompanyEmail
        {
            get => textBlockCompanyEmail;
            set
            {
                textBlockCompanyEmail = value;
                OnPropertyChanged(nameof(TextBlockCompanyEmail));
            }
        }

        private List<ExternalDocumentFile> DocumentFilesList { get; set; }
        
        public ObservableCollection<ExternalDocumentFile> DocumentFiles { get; set; }

        public ExternalDocumentFile SelectedDocumentFile {  get; set; }

        private Employee EmployeeReceivedDocument { get; set; }

        private string registerOrUpdateBtnTitle = "Зарегистрировать";
        public string RegisterOrUpdateBtnTitle
        {
            get => registerOrUpdateBtnTitle;
            set
            {
                registerOrUpdateBtnTitle = value;
                OnPropertyChanged(nameof(RegisterOrUpdateBtnTitle));
            }
        }

        public DocumentViewModel(DocumentWindow window, ExternalDocument selectedDocument)
        {
            DocumentWindow = window;
            SelectedDocument = selectedDocument;
            fileDialogService = new WindowsDialogService();
            DocumentFilesList = new List<ExternalDocumentFile>();
            DocumentFiles = new ObservableCollection<ExternalDocumentFile>();
            if (SelectedDocument != null)
            {
                DocumentTitle = SelectedDocument.ExternalDocumentTitle;
                DocumentNumber = SelectedDocument.ExternalDocumentNumber;
                DocumentDate = SelectedDocument.ExternalDocumentDate;
                ContractorCompany = SelectedDocument.ContractorCompany;
                TextBlockCompanyTitle = SelectedDocument.ContractorCompany.ContractorCompanyTitle;
                TextBlockCompanyShortTitle = SelectedDocument.ContractorCompany.ContractorCompanyShortTitle;
                TextBlockCompanyAddress = $"Юридический адрес: {SelectedDocument.ContractorCompany.ContractorCompanyAddress}";
                TextBlockCompanyPhone = $"Контактный телефон: {SelectedDocument.ContractorCompany.ContractorCompanyPhone}";
                TextBlockCompanyEmail = $"Электронная почта: {SelectedDocument.ContractorCompany.ContractorCompanyEmail}";
                ContractorCompanyID = SelectedDocument.ContractorCompanyID;
                SelectedDocumentType = SelectedDocument.ExternalDocumentType;
                SelectedDocumentTypeIndex = ExternalDocumentTypeConverter.ToIntConvert(SelectedDocument.ExternalDocumentType);
                EmployeeReceivedDocument = SelectedDocument.EmployeeReceivedDocument;
                RegisterOrUpdateBtnTitle = "Сохранить изменения";
                DocumentWindow.ButtonAddCompany.Visibility = Visibility.Hidden;
                DocumentWindow.ButtonChangeCompany.Visibility = Visibility.Visible;
                GetDocumentFiles();
                InitializeDocumentFiles();
            }
            else
            {
                DocumentDate = DateTime.Now;
                DocumentFilesList = new List<ExternalDocumentFile>();
                SelectedDocumentType = ExternalDocumentType.Contract;
            } 
            InitializeDocumentTypes();
        }

        private void InitializeDocumentTypes()
        {
            DocumentTypes = new List<string>();
            var documentTypes = Enum.GetValues(typeof(ExternalDocumentType));
            foreach (var type in documentTypes)
            {
                DocumentTypes.Add(ExternalDocumentTypeConverter.ConvertToString(type));
            }
        }

        public ICommand IOpenContractorCompanyListWindow => new RelayCommand(openContractorCompanyListWindow => OpenContractorCompanyListWindow());
        private void OpenContractorCompanyListWindow()
        {
            ContractorСompanyListWindow contractorСompanyListWindow = new ContractorСompanyListWindow();
            contractorСompanyListWindow.ShowDialog();
            if (contractorСompanyListWindow.viewModel.SelectedContractorCompany != null)
            {
                ContractorCompany = contractorСompanyListWindow.viewModel.SelectedContractorCompany;
                OnPropertyChanged(nameof(ContractorCompany));
                TextBlockCompanyTitle = ContractorCompany.ContractorCompanyTitle;
                TextBlockCompanyShortTitle = ContractorCompany.ContractorCompanyShortTitle;
                TextBlockCompanyAddress = $"Юридический адрес: {ContractorCompany.ContractorCompanyAddress}";
                TextBlockCompanyPhone = $"Контактный телефон: {ContractorCompany.ContractorCompanyPhone}";
                TextBlockCompanyEmail = $"Электронная почта: {ContractorCompany.ContractorCompanyEmail}";
            }
        }

        private void GetDocumentFiles()
        {
            if(SelectedDocument != null)
            {
                DocumentFilesList.Clear();
                ExternalDocumentFileController documentFileController = new ExternalDocumentFileController();
                DocumentFilesList = documentFileController.GetDocumentFiles(SelectedDocument.ExternalDocumentID);
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
            SelectedDocumentFile = DocumentFiles.FirstOrDefault();
        }

        public ICommand IBrowseDocumentFiles => new RelayCommand(browseDocumentFiles => BrowseDocumentFile());
        private void BrowseDocumentFile()
        {
            var filePath = fileDialogService.OpenFile();
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            ExternalDocumentFile documentFile = new ExternalDocumentFile();
            documentFile.ExternalFileName = fileName;
            documentFile.ExternalFileExtension = fileExtension;
            documentFile.ExternalFileData = fileData;
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
                    ExternalDocumentFileController documentFileController = new ExternalDocumentFileController();
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

        public ICommand IRegisterOrRenewDocument => new RelayCommand(registerOrRenewDocument => RegisterOrRenewDocument());
        private void RegisterOrRenewDocument()
        {
            if (!ValidationDocument()) return;
            if (SelectedDocument == null) RegisterDocument();
            else UpdateDocument();
        }

        private void RegisterDocument()
        {
            ExternalDocument newDocument = CreateDocument();
            bool result = false;
            ExternalDocumentController documentController = new ExternalDocumentController();
            if (SelectedDocument == null)
            {
                newDocument.RegistrationDate = DateTime.Now;
                newDocument.IsRegistated = true;
            }
            result = documentController.AddDocument(newDocument);
            if (result)
            {
                MessageBox.Show("Документ зарегистрирован");
                DocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в регистрации документа");
            }
        }

        private void UpdateDocument()
        {
            SelectedDocument.ExternalDocumentTitle = DocumentTitle;
            SelectedDocument.ExternalDocumentNumber = DocumentNumber;
            SelectedDocument.ExternalDocumentDate = DocumentDate;
            SelectedDocument.ContractorCompany = ContractorCompany;
            SelectedDocument.ExternalDocumentType = SelectedDocumentType;
            SelectedDocument.ExternalDocumentFiles = DocumentFiles.ToList();
            SelectedDocument.IsRegistated = true;
            if(SelectedDocument.EmployeeReceivedDocument == null || SelectedDocument.EmployeeReceivedDocument != EmployeeReceivedDocument)
            {
                SelectedDocument.EmployeeReceivedDocument = EmployeeReceivedDocument;
                SelectedDocument.SendingDate = DateTime.Now;
            }
            bool result = false;
            ExternalDocumentController documentController = new ExternalDocumentController();
            result = documentController.UpdateDocument(SelectedDocument);
            if (result)
            {
                MessageBox.Show("Документ обновлен");
                DocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в обновлении документа");
            }
        }

        private ExternalDocument CreateDocument()
        {
            ExternalDocument newDocument = new ExternalDocument();
            newDocument.ExternalDocumentTitle = DocumentTitle;
            newDocument.ExternalDocumentNumber = DocumentNumber;
            newDocument.ExternalDocumentDate = DocumentDate;
            newDocument.ContractorCompany = ContractorCompany;
            newDocument.ExternalDocumentType = SelectedDocumentType;
            newDocument.ExternalDocumentFiles = DocumentFiles.ToList();
            newDocument.IsRegistated = false;
            if(SelectedDocument != null)
            {
                newDocument.ContractorCompany = SelectedDocument.ContractorCompany;
                newDocument.ExternalDocumentID = SelectedDocument.ExternalDocumentID;
                newDocument.RegistrationDate = SelectedDocument.RegistrationDate;
                newDocument.IsRegistated = SelectedDocument.IsRegistated;
            }
            return newDocument;
        }

        public ICommand IRemoveDocument => new RelayCommand(removeDocument => RemoveDocument());
        private void RemoveDocument()
        {
            DocumentTitle = string.Empty;
            DocumentNumber = string.Empty;
            DocumentDate = DateTime.Now;
            TextBlockCompanyTitle = "Наименование контрагента";
            TextBlockCompanyShortTitle = "Сокращенное наименование контрагента";
            TextBlockCompanyAddress = "Юридический адрес: ";
            TextBlockCompanyPhone = "Контактный телефон: ";
            TextBlockCompanyEmail = "Электронная почта: ";
            SelectedDocumentType = ExternalDocumentType.Contract;
            SelectedDocumentTypeIndex = 0;
            DocumentFilesList.Clear();
            DocumentFiles.Clear();
            if (SelectedDocument != null)
            {
                ExternalDocumentController documentController = new ExternalDocumentController();
                documentController.RemoveDocument(SelectedDocument);
            }
            DocumentWindow.Close();
        }

        public ICommand IExit => new RelayCommand(exit => DocumentWindow.Close());

        public ICommand ISendToExaminingPerson => new RelayCommand(sendToExaminingPerson => SendToExaminingPerson());
        private void SendToExaminingPerson()
        {
            if (!ValidationDocument()) return;
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true, null);
            examiningPersonsWindow.ShowDialog();
            bool result = false;
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                ExternalDocument document = CreateDocument();
                document.EmployeeReceivedDocument = examiningPersonsWindow.viewModel.SelectedEmployee;
                ExternalDocumentController documentController = new ExternalDocumentController();
                if (document.IsRegistated == false)
                {
                    document.RegistrationDate = DateTime.Now;
                    document.SendingDate = DateTime.Now;
                    document.IsRegistated = true;
                    result = documentController.AddDocument(document);
                }
                else
                {
                    document.SendingDate = DateTime.Now;
                    result = documentController.UpdateDocument(document);
                }
            }
            if (result)
            {
                MessageBox.Show("Документ успешно направлен");
                DocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в отправке документа");
            }
        }

        private bool ValidationDocument()
        {
            if (string.IsNullOrEmpty(DocumentTitle))
            {
                MessageBox.Show("Введите наименование документа");
                DocumentWindow.DocumentTitle.BorderThickness = new System.Windows.Thickness(2);
                DocumentWindow.DocumentTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                DocumentWindow.DocumentTitle.BorderThickness = new System.Windows.Thickness(1);
                DocumentWindow.DocumentTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (DocumentDate > DateTime.Now)
            {
                MessageBox.Show("Дата документа позднее текущей даты");
                DocumentWindow.DocumentDate.BorderThickness = new System.Windows.Thickness(2);
                DocumentWindow.DocumentDate.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                DocumentWindow.DocumentDate.BorderThickness = new System.Windows.Thickness(2);
                DocumentWindow.DocumentDate.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            if (DocumentFiles == null || DocumentFiles.Count == 0)
            {
                MessageBox.Show("К документу не прикреплены файлы");
                DocumentWindow.FileList.BorderThickness = new System.Windows.Thickness(2);
                DocumentWindow.FileList.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                DocumentWindow.FileList.BorderThickness = new System.Windows.Thickness(2);
                DocumentWindow.FileList.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            if (ContractorCompany == null)
            {
                MessageBox.Show("Не выбран контрагент");
                DocumentWindow.CompanyInfo.BorderBrush = new SolidColorBrush(Colors.Red);
                DocumentWindow.CompanyInfo.BorderThickness = new System.Windows.Thickness(2);
                return false;
            }
            else
            {
                DocumentWindow.CompanyInfo.BorderBrush = new SolidColorBrush(Colors.Transparent);
                DocumentWindow.CompanyInfo.BorderThickness = new System.Windows.Thickness(2);
            }
            return true;
        }
    }
}
