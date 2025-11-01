﻿using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagment.UI.ViewModels
{
    public class InternalDocumentViewModel : BaseViewModelClass
    {
        private InternalDocumentWindow InternalDocumentWindow;
        private InternalDocument InternalDocument;
        private IFileDialogService fileDialogService;
        public List<string> InternalDocumentTypes { get; set; }

        private InternalDocumentTypes selectedInternalDocumentType;

        public InternalDocumentTypes SelectedInternalDocumentType
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

        private string approvedManagerDepartment;
        public string ApprovedManagerDepartment
        {
            get => approvedManagerDepartment;
            set
            {
                approvedManagerDepartment = value;
                OnPropertyChanged(nameof(ApprovedManagerDepartment));
            }
        }
        private string approvedManagerPosition;
        public string ApprovedManagerPosition
        {
            get => approvedManagerPosition;
            set
            {
                approvedManagerPosition = value;
                OnPropertyChanged(nameof(ApprovedManagerPosition));
            }
        }
        private string approvedManagerFullName;
        public string ApprovedManagerFullName
        {
            get => approvedManagerFullName;
            set
            {
                approvedManagerFullName = value;
                OnPropertyChanged(nameof(ApprovedManagerFullName));
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

        private string signatoryDepartment;
        public string SignatoryDepartment
        {
            get => signatoryDepartment;
            set
            {
                signatoryDepartment = value;
                OnPropertyChanged(nameof(SignatoryDepartment));
            }
        }
        private string signatoryPosition;
        public string SignatoryPosition
        {
            get => signatoryPosition;
            set
            {
                signatoryPosition = value;
                OnPropertyChanged(nameof(SignatoryPosition));
            }
        }
        private string signatoryFullName;
        public string SignatoryFullName
        {
            get => signatoryFullName;
            set
            {
                signatoryFullName = value;
                OnPropertyChanged(nameof(SignatoryFullName));
            }
        }

        private Employee EmployeeRecievedDocument { get; set; }
        private List<InternalDocumentFile> InternalDocumentFilesList { get; set; }

        public ObservableCollection<InternalDocumentFile> InternalDocumentFiles { get; set; }

        public InternalDocumentFile SelectedInternalDocumentFile { get; set; }

        private List<Department> Departments;

        private string registerOrUpdateBtnTitle = "Зарегистрировать документ";
        public string RegisterOrUpdateBtnTitle
        {
            get => registerOrUpdateBtnTitle;
            set
            {
                registerOrUpdateBtnTitle = value;
                OnPropertyChanged(nameof(RegisterOrUpdateBtnTitle));
            }
        }

        public InternalDocumentViewModel(InternalDocumentWindow internalDocumentWindow, InternalDocument internalDocument = null)
        {
            InternalDocumentWindow = internalDocumentWindow;
            InternalDocument = internalDocument;
            fileDialogService = new WindowsDialogService();
            InternalDocumentDate = DateTime.Now;
            InternalDocumentTypes = new List<string>();
            InternalDocumentFilesList = new List<InternalDocumentFile>();
            InternalDocumentFiles = new ObservableCollection<InternalDocumentFile>();
            Departments = new List<Department>();
            InitializeInternalDocumentTypes();
            GetInternalDocumentFiles();
            InitializeInternalDocumentFiles();
            InitializeDepartments();
            if (InternalDocument != null)
            {
                RegisterOrUpdateBtnTitle = "Сохранить изменения";
                SelectedInternalDocumentType = InternalDocument.InternalDocumentType;
                SelectedInternalDocumentTypeIndex = InternalDocumentTypeConverter.ToIntConvert(InternalDocument.InternalDocumentType);
                if (InternalDocument.ApprovedManagerID != 0)
                {
                    EmployeeController controller = new EmployeeController();
                    ApprovedManager = controller.GetEmployeeByID(InternalDocument.ApprovedManagerID);
                    if(ApprovedManager != null)
                    {
                        DepartmentController departmentController = new DepartmentController();
                        Department department = departmentController.GetDepartmentByID(ApprovedManager.DepartmentID);
                        ApprovedManager.Department = department;
                    }
                    ApprovedManagerDepartment = ApprovedManager.Department.DepartmentTitle;
                    ApprovedManagerPosition = ApprovedManager.Position;
                    ApprovedManagerFullName = ApprovedManager.EmployeeFullName;

                }
                if (InternalDocument.SignatoryID != 0)
                {
                    EmployeeController controller = new EmployeeController();
                    Signatory = controller.GetEmployeeByID(InternalDocument.SignatoryID);
                    if (Signatory != null)
                    {
                        DepartmentController departmentController = new DepartmentController();
                        Department department = departmentController.GetDepartmentByID(Signatory.DepartmentID);
                        Signatory.Department = department;
                    }
                    SignatoryDepartment = Signatory.Department.DepartmentTitle;
                    SignatoryPosition = Signatory.Position;
                    SignatoryFullName = Signatory.EmployeeFullName;
                }
                if(InternalDocument.EmployeeRecievedDocumentID != 0)
                {
                    EmployeeController controller = new EmployeeController();
                    EmployeeRecievedDocument = controller.GetEmployeeByID(InternalDocument.EmployeeRecievedDocumentID);
                }
                InternalDocumentDate = InternalDocument.InternalDocumentDate;
            }
        }

        private void InitializeInternalDocumentTypes()
        {
            InternalDocumentTypes = new List<string>();
            var internalDocumentTypes = Enum.GetValues(typeof(InternalDocumentTypes));
            foreach (var type in internalDocumentTypes)
            {
                InternalDocumentTypes.Add(InternalDocumentTypeConverter.ConvertToString(type));
            }
        }

        private void GetInternalDocumentFiles()
        {
            if (InternalDocument == null) return;
            InternalDocumentFilesList.Clear();
            InternalDocumentFileController fileController = new InternalDocumentFileController();
            InternalDocumentFilesList = fileController.GetInternalDocumentFiles(InternalDocument.InternalDocumentID);
            SelectedInternalDocumentFile = InternalDocumentFiles.FirstOrDefault();
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

        private void InitializeDepartments()
        {
            Departments.Clear();
            DepartmentController departmentController = new DepartmentController();
            Departments = departmentController.GetAllDepartments();
        }

        public ICommand IOpenApprovedManagerWindow => new RelayCommand(openApprovedManagerWindow => OpenApprovedManagerWindow());
        private void OpenApprovedManagerWindow()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true);
            examiningPersonsWindow.ShowDialog();
            if(examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                ApprovedManager = examiningPersonsWindow.viewModel.SelectedEmployee;
                InitializeApprovedManagerData();
            }
        }

        private void InitializeApprovedManagerData()
        {
            if (ApprovedManager != null)
            {
                ApprovedManagerDepartment = ApprovedManager.Department.DepartmentTitle;
                ApprovedManagerPosition = ApprovedManager.Position;
                ApprovedManagerFullName = ApprovedManager.EmployeeFullName;
            }
        }

        public ICommand IOpenSignatoryWindow => new RelayCommand(openSignatoryWindow => OpenSignatoryWindow());
        private void OpenSignatoryWindow()
        {
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(false);
            examiningPersonsWindow.ShowDialog();
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                Signatory = examiningPersonsWindow.viewModel.SelectedEmployee;
                InitializeSignatoryData();
            }
        }

        private void InitializeSignatoryData()
        {
            if (Signatory != null)
            {
                SignatoryDepartment = Signatory.Department.DepartmentTitle;
                SignatoryPosition = Signatory.Position;
                SignatoryFullName = Signatory.EmployeeFullName;
            }
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
            InternalDocumentFilesList.Add(internalDocumentFile);
            InitializeInternalDocumentFiles();
        }

        public ICommand IDeleteInternalDocumentFile => new RelayCommand(deleteInternalDocumentFile => DeleteInternalDocumentFile());
        private void DeleteInternalDocumentFile()
        {
            bool result = false;
            if (InternalDocument != null)
            {
                if (SelectedInternalDocumentFile != null)
                {
                    InternalDocumentFileController internalDocumentFileController = new InternalDocumentFileController();
                    result = internalDocumentFileController.RemoveInternalDocumentFile(SelectedInternalDocumentFile);
                }
                if (result)
                {
                    MessageBox.Show("Удаление файла выполнено");
                    GetInternalDocumentFiles();
                    InitializeInternalDocumentFiles();
                    return;
                }
                else
                {
                    MessageBox.Show("Ошибка. Удаление файла не выполнено!");
                }
            }
            else
            {
                InternalDocumentFilesList.Remove(SelectedInternalDocumentFile);
                InitializeInternalDocumentFiles();
                SelectedInternalDocumentFile = InternalDocumentFiles.FirstOrDefault();
            }
        }

        public ICommand IRegisterOrUpdateInternalDocument => new RelayCommand(registerOrUpdateInternalDocument => RegisterOrUpdateInternalDocument());
        private void RegisterOrUpdateInternalDocument()
        {
            if (!ValidationInternalDocument()) return;
            if (InternalDocument == null) RegisterInternalDocument();
            else UpdateInternalDocument();
        }
        
        private void RegisterInternalDocument()
        {
            InternalDocument newInternalDocument = CreateInternalDocument();
            bool result = false;
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            if (InternalDocument == null)
            {
                newInternalDocument.RegistrationDate = DateTime.Now;
                newInternalDocument.IsRegistated = true;
            }
            if (internalDocumentController.AddInternalDocument(newInternalDocument))
            { 
                MessageBox.Show("Документ зарегистрирован");
                InternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в регистрации документа");
                InternalDocumentWindow.Close();
            }
        }

        private void UpdateInternalDocument()
        {
            InternalDocument.InternalDocumentType = SelectedInternalDocumentType;
            InternalDocument.InternalDocumentDate = InternalDocumentDate;
            InternalDocument.Signatory = Signatory;
            InternalDocument.SignatoryID = Signatory.EmployeeID;
            InternalDocument.ApprovedManager = ApprovedManager;
            InternalDocument.ApprovedManagerID = ApprovedManager.EmployeeID;
            InternalDocument.EmployeeRecievedDocument = EmployeeRecievedDocument;
            InternalDocument.EmployeeRecievedDocumentID = EmployeeRecievedDocument.EmployeeID;
            InternalDocument.InternalDocumentFiles = InternalDocumentFiles.ToList();
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            if (internalDocumentController.UpdateInternalDocument(InternalDocument))
            {
                MessageBox.Show("Изменения сохранены");
                InternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при внесении изменений");
                InternalDocumentWindow.Close();
            }
        }

        private InternalDocument CreateInternalDocument()
        {
            InternalDocument newInternalDocument = new InternalDocument();
            newInternalDocument.InternalDocumentType = SelectedInternalDocumentType;
            newInternalDocument.SignatoryID = Signatory.EmployeeID;
            newInternalDocument.Signatory = Signatory;
            if (ApprovedManager != null)
            {
                newInternalDocument.ApprovedManagerID = ApprovedManager.EmployeeID;
                newInternalDocument.ApprovedManager = ApprovedManager;
            }
            newInternalDocument.InternalDocumentFiles = InternalDocumentFiles.ToList();
            newInternalDocument.InternalDocumentStatus = InternalDocumentStatus.Processing;
            newInternalDocument.InternalDocumentDate = InternalDocumentDate;
            if (InternalDocument != null)
            {
                newInternalDocument.InternalDocumentID = InternalDocument.InternalDocumentID;
                newInternalDocument.RegistrationDate = InternalDocument.RegistrationDate;
                newInternalDocument.IsRegistated = InternalDocument.IsRegistated;
            }
            return newInternalDocument;
        }

        public ICommand IRemoveInternalDocument => new RelayCommand(removeInternalDocument => RemoveInternalDocument());
        private void RemoveInternalDocument()
        {
            SelectedInternalDocumentTypeIndex = 0;
            InternalDocumentDate = DateTime.Now;
            ApprovedManager = null;
            Signatory = null;
            InternalDocumentFilesList.Clear();
            InternalDocumentFiles.Clear();
            if (InternalDocument != null)
            {
                InternalDocumentController internalDocumentController = new InternalDocumentController();
                internalDocumentController.RemoveInternalDocument(InternalDocument);
            }
        }

        public ICommand ISendToExaminingPerson => new RelayCommand(sendToExaminingPerson => SendToExaminingPerson());
        private void SendToExaminingPerson()
        {
            if (!ValidationInternalDocument()) return;
            ExaminingPersonsWindow examiningPersonsWindow = new ExaminingPersonsWindow(true);
            examiningPersonsWindow.ShowDialog();
            bool result = false;
            if (examiningPersonsWindow.viewModel.SelectedEmployee != null)
            {
                InternalDocument internalDocument = CreateInternalDocument();
                internalDocument.EmployeeRecievedDocumentID = examiningPersonsWindow.viewModel.SelectedEmployee.EmployeeID;
                internalDocument.EmployeeRecievedDocument = examiningPersonsWindow.viewModel.SelectedEmployee;
                EmployeeRecievedDocument = internalDocument.EmployeeRecievedDocument;
                InternalDocumentController internalDocumentController = new InternalDocumentController();
                if (internalDocument.IsRegistated == false)
                {
                    internalDocument.RegistrationDate = DateTime.Now;
                    internalDocument.SendingDate = DateTime.Now;
                    internalDocument.IsRegistated = true;
                    result = internalDocumentController.AddInternalDocument(internalDocument);
                }
                else
                {
                    internalDocument.SendingDate = DateTime.Now;
                    result = internalDocumentController.UpdateInternalDocument(internalDocument);
                }
            }
            if (result)
            {
                MessageBox.Show("Документ успешно направлен");
                InternalDocumentWindow.Close();
            }
            else
            {
                MessageBox.Show("Ошибка в отправке документа");
                InternalDocumentWindow.Close();
            }
        }

        private bool ValidationInternalDocument()
        {
            if (Signatory == null)
            {
                MessageBox.Show("Выберите подписанта внутреннего документа");
                InternalDocumentWindow.SignatoryInfo.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.SignatoryInfo.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                InternalDocumentWindow.SignatoryInfo.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.SignatoryInfo.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            if (InternalDocumentDate > DateTime.Now)
            {
                MessageBox.Show("Дата документа позже текущей");
                InternalDocumentWindow.InternalDocumentDate.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentDate.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                InternalDocumentWindow.InternalDocumentDate.BorderThickness = new System.Windows.Thickness(2);
                InternalDocumentWindow.InternalDocumentDate.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            return true;
        }

        public ICommand IExit => new RelayCommand(exit => InternalDocumentWindow.Close());
    }
}
