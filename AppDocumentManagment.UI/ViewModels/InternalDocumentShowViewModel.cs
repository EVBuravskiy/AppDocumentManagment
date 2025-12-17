using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class InternalDocumentShowViewModel : BaseViewModelClass
    {
        private InternalDocumentShowWindow InternalDocumentShowWindow { get; set; }

        private InternalDocument InternalDocument;

        private Employee CurrentEmployee;

        private IFileDialogService fileDialogService;

        private string internalDocumentType;
        public string InternalDocumentType
        {
            get => internalDocumentType;
            set
            {
                internalDocumentType = value;
                OnPropertyChanged(nameof(InternalDocumentType));
            }
        }
        private string internalDocumentDate;
        public string InternalDocumentDate
        {
            get => internalDocumentDate;
            set
            {
                internalDocumentDate = value;
                OnPropertyChanged(nameof(InternalDocumentDate));
            }
        }

        private Employee signatory;
        public Employee Signatory
        {
            get => signatory;
            set
            {
                signatory = value;
                OnPropertyChanged(nameof(Signatory));
            }
        }

        private Employee approvedManager;
        public Employee ApprovedManager
        {
            get => approvedManager;
            set
            {
                approvedManager = value;
                OnPropertyChanged(nameof(ApprovedManager));
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

        private List<InternalDocumentFile> InternalDocumentFilesList;

        public ObservableCollection<InternalDocumentFile> InternalDocumentFiles { get; set; }

        private InternalDocumentFile selectedInternalDocumentFile;
        public InternalDocumentFile SelectedInternalDocumentFile
        {
            get => selectedInternalDocumentFile;
            set
            {
                selectedInternalDocumentFile = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentFile));
                if(value != null)
                {
                    BrowseToSaveInternalDocumentFile();
                }
            }
        }

        public InternalDocumentShowViewModel(InternalDocumentShowWindow window, InternalDocument inputInternalDocument, int currentEmployeeID)
        {
            InternalDocumentShowWindow = window;
            fileDialogService = new WindowsDialogService();
            InternalDocument = inputInternalDocument;
            InternalDocumentFilesList = new List<InternalDocumentFile>();
            InternalDocumentFiles = new ObservableCollection<InternalDocumentFile>();
            if (InternalDocument != null)
            {
                InternalDocumentType = InternalDocumentTypeConverter.ConvertToString(InternalDocument.InternalDocumentType);
                InternalDocumentDate = DateConverter.ConvertDateToString(InternalDocument.InternalDocumentDate);
                if(InternalDocument.SignatoryID != 0)
                {
                    Signatory = GetEmployeeByID(InternalDocument.SignatoryID);
                }
                if (InternalDocument.ApprovedManagerID != 0)
                {
                    ApprovedManager = GetEmployeeByID(InternalDocument.ApprovedManagerID);
                }
                InternalDocumentTitle = InternalDocument.InternalDocumentTitle;
                InternalDocumentContent = InternalDocument.InternalDocumentContent;
                GetInternalDocumentFilesList();
                InitializeInternalDocumentFiles();
            }
            if (currentEmployeeID != 0)
            {
                CurrentEmployee = GetEmployeeByID(currentEmployeeID);
            }
            if(CurrentEmployee.EmployeeRole == EmployeeRole.Performer)
            {
                InternalDocumentShowWindow.InternalDocumentContent.Height = new GridLength(380, GridUnitType.Pixel);
                InternalDocumentShowWindow.InternalDocumentButtons.Height = new GridLength(0, GridUnitType.Pixel);
                InternalDocumentShowWindow.DocumentContent.Height = 330;
            }
        }

        private Employee GetEmployeeByID(int employeeID)
        {
            EmployeeController employeeController = new EmployeeController();
            Employee employee = employeeController.GetEmployeeByID(employeeID);
            if (employee != null && employee.DepartmentID != 0)
            {
                DepartmentController departmentController = new DepartmentController();
                Department department = departmentController.GetDepartmentByID(employee.DepartmentID);
                employee.Department = department;
            }
            return employee;
        }

        private void GetInternalDocumentFilesList()
        {
            InternalDocumentFilesList.Clear();
            if (InternalDocument != null)
            {
                InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
                InternalDocumentFilesList = internalDocumentFileController.GetInternalDocumentFiles(InternalDocument.InternalDocumentID);
            }
        }

        private void InitializeInternalDocumentFiles()
        {
            InternalDocumentFiles.Clear();
            if (InternalDocumentFilesList.Count > 0)
            {
                foreach (InternalDocumentFile file in InternalDocumentFilesList)
                {
                    InternalDocumentFiles.Add(file);
                }
            }
        }

        public ICommand IBrowseToSaveInternalDocumentFile => new RelayCommand(browseToSaveInternalDocument => BrowseToSaveInternalDocumentFile());
        private void BrowseToSaveInternalDocumentFile()
        {
            var filePath = fileDialogService.SaveFile(SelectedInternalDocumentFile.FileExtension, SelectedInternalDocumentFile.FileName);
            bool result = FileProcessing.SaveInternalDocumentFileToPath(filePath, SelectedInternalDocumentFile);
            if (result)
            {
                MessageBox.Show($"Файл {SelectedInternalDocumentFile.FileName} сохранен");
            }
            else
            {
                MessageBox.Show($"Файл {SelectedInternalDocumentFile.FileName} уже имеется, либо не был сохранен");
            }
        }

        public ICommand ILoadInternalDocumentFiles => new RelayCommand(loadInternalDocumentFiles => LoadInternalDocumentFiles());
        private void LoadInternalDocumentFiles()
        {
            string directoryPath = string.Empty;
            foreach (InternalDocumentFile file in InternalDocumentFiles)
            {
                directoryPath = FileProcessing.SaveInternalDocumentFileFromDB(file, "Internals");
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


        public ICommand IBrowseInternalDocumentFile => new RelayCommand(browseInternalDocumentFile => BrowseInternalDocumentFile());
        private void BrowseInternalDocumentFile()
        {
            var filePath = fileDialogService.OpenFile("Files (*.txt;*.jpg;*.jpeg;*.png;*.pdf)|*.txt;*.jpg;*.jpeg;*.png;*.pdf|All files");
            if (filePath == null) return;
            string fileName = FileProcessing.GetFileName(filePath);
            string fileExtension = FileProcessing.GetFileExtension(filePath);
            byte[] fileData = FileProcessing.GetFileData(filePath);
            InternalDocumentFile internalDocumentFile = new InternalDocumentFile();
            internalDocumentFile.FileName = fileName;
            internalDocumentFile.FileExtension = fileExtension;
            internalDocumentFile.FileData = fileData;
            internalDocumentFile.InternalDocument = InternalDocument;
            InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
            bool result = internalDocumentFileController.AddInternalDocumentFile(internalDocumentFile, InternalDocument);
            if (result)
            {
                MessageBox.Show("Файл успешно добавлен");
            }
            else
            {
                MessageBox.Show("Ошибка! Файл не был добавлен");
            }
            GetInternalDocumentFilesList();
            InitializeInternalDocumentFiles();
        }

        public ICommand ISendToWork => new RelayCommand(sendToWork => SendToWork());

        private void SendToWork()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true, null);
            examiningPersonsWindow.ShowDialog();
            bool result = false;
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                InternalDocument.EmployeeRecievedDocument = examiningPersonsWindow.viewModel.SelectedEmployee;
                InternalDocument.EmployeeRecievedDocumentID = examiningPersonsWindow.viewModel.SelectedEmployee.EmployeeID;
                InternalDocument.SendingDate = DateTime.Now;
                InternalDocumentController internalDocumentController = new InternalDocumentController();
                result = internalDocumentController.UpdateInternalDocument(InternalDocument);
            }
            if (result)
            {
                MessageBox.Show("Документ успешно направлен для исполнения");
                InternalDocumentShowWindow.Close();
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
            ProductionTaskWindow productionTaskWindow = new ProductionTaskWindow(CurrentEmployee, null, InternalDocument);
            productionTaskWindow.ShowDialog();
        }

        public ICommand IAgreeInternalDocument => new RelayCommand(agreeInternalDocument => AgreeInternalDocument());
        private void AgreeInternalDocument()
        {
            bool result = false;    
            if (CurrentEmployee != null)
            {
                InternalDocument.ApprovedManager = CurrentEmployee;
                InternalDocument.ApprovedManagerID = CurrentEmployee.EmployeeID;
                InternalDocument.InternalDocumentStatus = DocumentStatus.Agreed;
                InternalDocumentController internalDocumentController = new InternalDocumentController();
                result = internalDocumentController.UpdateInternalDocument(InternalDocument);
                ApprovedManager = CurrentEmployee;
            }
            if (result)
            {
                MessageBox.Show($"Документ был согласован: {CurrentEmployee.EmployeeFullName}");
                InternalDocumentShowWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в согласовании документа");
            }

        }

        public ICommand IExit => new RelayCommand(exit => { InternalDocumentShowWindow.Close(); });

    }
}
