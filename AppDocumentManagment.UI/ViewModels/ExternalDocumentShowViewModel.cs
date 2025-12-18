using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ExternalDocumentShowViewModel : BaseViewModelClass
    {
        ExternalDocumentShowWindow ExternalDocumentShowWindow;

        ExternalDocument ExternalDocument;

        Employee CurrentEmployee;

        private IFileDialogService fileDialogService;

        private string documentType;
        public string DocumentType
        {
            get => documentType;
            set
            {
                documentType = value;
                OnPropertyChanged(nameof(DocumentType));
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

        private string textBlockCompanyTitle;
        public string TextBlockCompanyTitle
        {
            get => textBlockCompanyTitle;
            set
            {
                textBlockCompanyTitle = value;
                OnPropertyChanged(nameof(TextBlockCompanyTitle));
            }
        }

        private string textBlockCompanyShortTitle;
        public string TextBlockCompanyShortTitle
        {
            get => textBlockCompanyShortTitle;
            set
            {
                textBlockCompanyShortTitle = value;
                OnPropertyChanged(nameof(TextBlockCompanyShortTitle));
            }
        }

        private string textBlockCompanyAddress;
        public string TextBlockCompanyAddress
        {
            get => textBlockCompanyAddress;
            set
            {
                textBlockCompanyAddress = value;
                OnPropertyChanged(nameof(TextBlockCompanyAddress));
            }
        }
        private string textBlockCompanyPhone;
        public string TextBlockCompanyPhone
        {
            get => textBlockCompanyPhone;
            set
            {
                textBlockCompanyPhone = value;
                OnPropertyChanged(nameof(TextBlockCompanyPhone));
            }
        }

        private string textBlockCompanyEmail;
        public string TextBlockCompanyEmail
        {
            get => textBlockCompanyEmail;
            set
            {
                textBlockCompanyEmail = value;
                OnPropertyChanged(nameof(TextBlockCompanyEmail));
            }
        }

        private List<ExternalDocumentFile> ExternalDocumentFilesList;

        public ObservableCollection<ExternalDocumentFile> ExternalDocumentFiles { get; set; }

        private ExternalDocumentFile selectedExternalDocumentFile;
        public ExternalDocumentFile SelectedExternalDocumentFile
        {
            get => selectedExternalDocumentFile;
            set
            {
                selectedExternalDocumentFile = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentFile));
                if (value != null)
                {
                    BrowseToSaveExternalDocumentFile();
                }
            }
        }

        public ExternalDocumentShowViewModel(ExternalDocumentShowWindow externalDocumentShowWindow, ExternalDocument inputExternalDocument, int documentContractorCompanyID, Employee currentEmployee)
        {
            ExternalDocumentShowWindow = externalDocumentShowWindow;
            fileDialogService = new WindowsDialogService();
            ExternalDocument = inputExternalDocument;
            CurrentEmployee = currentEmployee;
            if (documentContractorCompanyID != 0)
            {
                ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
                ContractorCompany company = contractorCompanyController.GetContractorCompanyByID(documentContractorCompanyID);
                if (company != null)
                {
                    ExternalDocument.ContractorCompany = company;
                }
            }
            ExternalDocumentFilesList = new List<ExternalDocumentFile>();
            ExternalDocumentFiles = new ObservableCollection<ExternalDocumentFile>();
            if (inputExternalDocument != null)
            {
                DocumentType = ExternalDocumentTypeConverter.ConvertToString(ExternalDocument.ExternalDocumentType);
                DocumentNumber = inputExternalDocument.ExternalDocumentNumber;
                DocumentTitle = inputExternalDocument.ExternalDocumentTitle;
                if (ExternalDocument.ContractorCompany != null)
                {
                    TextBlockCompanyTitle = ExternalDocument.ContractorCompany.ContractorCompanyTitle;
                    TextBlockCompanyShortTitle = ExternalDocument.ContractorCompany.ContractorCompanyShortTitle;
                    TextBlockCompanyAddress = $"Юридический адрес: {ExternalDocument.ContractorCompany.ContractorCompanyAddress}";
                    TextBlockCompanyPhone = $"Контактный телефон: {ExternalDocument.ContractorCompany.ContractorCompanyPhone}";
                    TextBlockCompanyEmail = $"Адрес электронной почты: {ExternalDocument.ContractorCompany.ContractorCompanyEmail}";
                }
                GetExternalDocumentFiles();
                InitializeExternalDocumentFiles();
                if(CurrentEmployee.EmployeeRole == EmployeeRole.Performer)
                {
                    ExternalDocumentShowWindow.ExternalDocumentFiles.Height = new GridLength(420, GridUnitType.Pixel);
                    ExternalDocumentShowWindow.ExternalDocumentButtons.Height = new GridLength(0, GridUnitType.Pixel);
                }
            }
        }

        private void GetExternalDocumentFiles()
        {
            ExternalDocumentFilesList.Clear();
            ExternalDocumentFileController documentFileController = new ExternalDocumentFileController();
            ExternalDocumentFilesList = documentFileController.GetDocumentFiles(ExternalDocument.ExternalDocumentID);
        }

        public void InitializeExternalDocumentFiles()
        {
            ExternalDocumentFiles.Clear();
            if (ExternalDocumentFilesList.Count > 0)
            {
                foreach (ExternalDocumentFile file in ExternalDocumentFilesList)
                {
                    ExternalDocumentFiles.Add(file);
                }
            }
        }

        public ICommand IBrowseToSaveExternalDocumentFile => new RelayCommand(browseToSaveExternalDocument => BrowseToSaveExternalDocumentFile());
        private void BrowseToSaveExternalDocumentFile()
        {
            var filePath = fileDialogService.SaveFile(SelectedExternalDocumentFile.ExternalFileExtension, SelectedExternalDocumentFile.ExternalFileName);
            bool result = FileProcessing.SaveExternalDocumentFileToPath(filePath, SelectedExternalDocumentFile);
            if (result)
            {
                MessageBox.Show($"Файл {SelectedExternalDocumentFile.ExternalFileName} сохранен");
            }
            else
            {
                MessageBox.Show($"Файл {SelectedExternalDocumentFile.ExternalFileName} уже имеется, либо не был сохранен");
            }
        }

        public ICommand ILoadExternalDocumentFiles => new RelayCommand(loadExternalDocumentFiles => LoadExternalDocumentFiles());
        private void LoadExternalDocumentFiles()
        {
            string directoryPath = string.Empty;
            foreach (ExternalDocumentFile file in ExternalDocumentFiles)
            {
                directoryPath = FileProcessing.SaveExternalDocumentFileFromDB(file, "ExternalDocuments");
            }
            if (string.IsNullOrEmpty(directoryPath))
            {
                MessageBox.Show("Не удалось сохранить файлы");
            }
            else
            {
                DirectoryProcessing.OpenDirectory(directoryPath);
            }
        }

        public ICommand IBrowseExternalDocumentFile => new RelayCommand(browseExternalDocumentFile => BrowseExternalDocumentFile());
        private void BrowseExternalDocumentFile()
        {
            var filePath = fileDialogService.OpenFile("Files|*.txt;*.jpg;*.jpeg;*.png;*.pdf|All files");
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            ExternalDocumentFile documentFile = new ExternalDocumentFile();
            documentFile.ExternalFileName = fileName;
            documentFile.ExternalFileExtension = fileExtension;
            documentFile.ExternalFileData = fileData;
            documentFile.ExternalDocument = ExternalDocument;
            ExternalDocumentFileController externalDocumentFileController = new ExternalDocumentFileController();
            externalDocumentFileController.AddExternalDocumentFile(documentFile, ExternalDocument);
            GetExternalDocumentFiles();
            InitializeExternalDocumentFiles();
        }

        public ICommand ISendToWork => new RelayCommand(sendToWork => SendToWork());

        private void SendToWork()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true, null);
            examiningPersonsWindow.ShowDialog();
            bool result = false;
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                ExternalDocument.EmployeeReceivedDocument = examiningPersonsWindow.viewModel.SelectedEmployee;
                ExternalDocument.SendingDate = DateTime.Now;
                ExternalDocumentController documentController = new ExternalDocumentController();
                result = documentController.UpdateDocument(ExternalDocument);
            }
            if (result)
            {
                MessageBox.Show("Документ успешно направлен для исполнения");
                ExternalDocumentShowWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в отправке документа");
            }
        }

        //TODO: Реализовать создание задачи к документу
        public ICommand ICreateTask => new RelayCommand(createTask => CreateTask());
        private void CreateTask()
        {
            ProductionTaskWindow productionTaskWindow = new ProductionTaskWindow(CurrentEmployee, ExternalDocument, null);
            productionTaskWindow.ShowDialog();
        }

        public ICommand IExit => new RelayCommand(exit => { ExternalDocumentShowWindow.Close(); });
    }
}
