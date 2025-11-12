using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Input;
using System.Xml.Linq;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ManagerPanelViewModel : BaseViewModelClass
    {
        private ManagerPanelWindow ManagerPanelWindow { get; set; }

        private Employee currentUser;

        private string greating;
        public string Greating
        {
            get => greating;
            set
            {
                greating = value;
                OnPropertyChanged(nameof(Greating));
            }
        }

        public ObservableCollection<string> ExternalDocumentStatus { get; set; }

        private int selectedExternalDocumentStatusIndex;
        public int SelectedExternalDocumentStatusIndex
        {
            get => selectedExternalDocumentStatusIndex;
            set
            {
                selectedExternalDocumentStatusIndex = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentStatusIndex));
            }
        }

        private string selectedExternalDocumentStatus;
        public string SelectedExternalDocumentStatus
        {
            get => selectedExternalDocumentStatus;
            set
            {
                selectedExternalDocumentStatus = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentStatus));
                if (!IsInternalDocument)
                {
                    if (SearchString.IsNullOrEmpty())
                    {
                        GetExternalDocumentsByDocumentType();
                    }
                    else
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                }
            }
        }

        public ObservableCollection<string> ExternalDocumentTypes { get; set; }

        private int selectedExternalDocumentTypeIndex;
        public int SelectedExternalDocumentTypeIndex
        {
            get => selectedExternalDocumentTypeIndex;
            set
            {
                selectedExternalDocumentTypeIndex = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentTypeIndex));
            }
        }

        private string selectedExternalDocumentType;
        public string SelectedExternalDocumentType
        {
            get => selectedExternalDocumentType;
            set
            {
                selectedExternalDocumentType = value;
                OnPropertyChanged(nameof(SelectedExternalDocumentType));
                if (!IsInternalDocument)
                {
                    if (SearchString.IsNullOrEmpty())
                    {
                        GetExternalDocumentsByDocumentType();
                    }
                    else
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                }
            }
        }

        public ObservableCollection<string> InternalDocumentStatus { get; set; }

        private int selectedInternalDocumentStatusIndex;
        public int SelectedInternalDocumentStatusIndex
        {
            get => selectedInternalDocumentStatusIndex;
            set
            {
                selectedInternalDocumentStatusIndex = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentStatusIndex));
            }
        }

        private string selectedInternalDocumentStatus;
        public string SelectedInternalDocumentStatus
        {
            get => selectedInternalDocumentStatus;
            set
            {
                selectedInternalDocumentStatus = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentStatus));
                if (IsInternalDocument)
                {
                    if (SearchString.IsNullOrEmpty())
                    {
                        GetInternalDocumentsByDocumentType();
                    }
                    else
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                }
            }
        }

        public ObservableCollection<string> InternalDocumentTypes { get; set; }

        private int selectedInternalDocumentTypeIndex;
        public int SelectedInternalDocumentTypeIndex
        {
            get => selectedInternalDocumentTypeIndex;
            set
            {
                selectedInternalDocumentTypeIndex = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentTypeIndex));
            }
        }

        private string selectedInternalDocumentType;
        public string SelectedInternalDocumentType
        {
            get => selectedInternalDocumentType;
            set
            {
                selectedInternalDocumentType = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentType));
                if (IsInternalDocument)
                {
                    if (SearchString.IsNullOrEmpty())
                    {
                        GetInternalDocumentsByDocumentType();
                    }
                    else
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                }
            }
        }

        private List<ExternalDocument> ExternalDocumentsList;
        public ObservableCollection<ExternalDocument> ExternalDocuments { get; set; }

        private ExternalDocument selectedExternalDocument;
        public ExternalDocument SelectedExternalDocument
        {
            get => selectedExternalDocument;
            set
            {
                selectedExternalDocument = value;
                OnPropertyChanged(nameof(SelectedExternalDocument));
            }
        }

        private List<InternalDocument> InternalDocumentsList;
        public ObservableCollection<InternalDocument> InternalDocuments { get; set; }

        private InternalDocument selectedInternalDocument;
        public InternalDocument SelectedInternalDocument
        {
            get => selectedInternalDocument;
            set
            {
                selectedInternalDocument = value;
                OnPropertyChanged(nameof(SelectedInternalDocument));
            }
        }

        private List<Employee> Employees;
        private List<Department> Departments;
        private List<ContractorCompany> ContractorCompanies;

        private string searchString;
        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }

        private string searchStringContent = "Поиск по наименованию документа или наименованию контрагента...";
        public string SearchStringContent
        {
            get => searchStringContent;
            set
            {
                searchStringContent = value;
                OnPropertyChanged(nameof(SearchStringContent));
            }
        }

        private bool IsInternalDocument = false;

        public ManagerPanelViewModel(ManagerPanelWindow window, int currentUserID)
        {
            ManagerPanelWindow = window;
            InitializeCurrentEmployee(currentUserID);
            ExternalDocumentsList = new List<ExternalDocument>();
            InternalDocumentsList = new List<InternalDocument>();
            Employees = new List<Employee>();
            Departments = new List<Department>();
            ContractorCompanies = new List<ContractorCompany>();
            ExternalDocumentStatus = new ObservableCollection<string>();
            InternalDocumentStatus = new ObservableCollection<string>();
            ExternalDocumentTypes = new ObservableCollection<string>();
            InternalDocumentTypes = new ObservableCollection<string>();
            ExternalDocuments = new ObservableCollection<ExternalDocument>();
            InternalDocuments = new ObservableCollection<InternalDocument>();
            InitializeInternalDocumentStatus();
            InitializeExternalDocumentStatus();
            InitializeExternalDocumentTypes();
            InitializeInternalDocumentTypes();
            GetEmployees();
            GetDepartments();
            GetContractorCompanies();
            GetExternalDocuments();
            GetInternalDocuments();
            InitializeExternalDocuments();
            InitializeInternalDocuments();
        }

        private void InitializeCurrentEmployee(int currentUserID)
        {
            if (currentUserID == 0) return;
            EmployeeController employeeController = new EmployeeController();
            currentUser = employeeController.GetEmployeeByID(currentUserID);
            Greating = $"Добрый день, {currentUser.EmployeeFirstMiddleName}!";
        }

        private void InitializeExternalDocumentStatus()
        {
            ExternalDocumentStatus.Clear();
            ExternalDocumentStatus = new ObservableCollection<string>
                {
                    "Все входящие документы"
                };
            var externalDocumentStatuses = Enum.GetValues(typeof(DocumentStatus));
            foreach (var externalDocumentStatus in externalDocumentStatuses)
            {
                ExternalDocumentStatus.Add(DocumentStatusConverter.ConvertToString(externalDocumentStatus));
            }
            SelectedExternalDocumentStatusIndex = 0;
            SelectedExternalDocumentStatus = ExternalDocumentStatus.FirstOrDefault();
        }

        private void InitializeInternalDocumentStatus()
        {
            InternalDocumentStatus.Clear();
            InternalDocumentStatus = new ObservableCollection<string>
                {
                    "Все внутренние документы"
                };
            var internalDocumentStatuses = Enum.GetValues(typeof(DocumentStatus));
            foreach (var internalDocumentStatus in internalDocumentStatuses)
            {
                InternalDocumentStatus.Add(DocumentStatusConverter.ConvertToString(internalDocumentStatus));
            }
            SelectedInternalDocumentStatusIndex = 0;
            SelectedInternalDocumentStatus = InternalDocumentStatus.FirstOrDefault();
        }

        private void InitializeExternalDocumentTypes()
        {
            ExternalDocumentTypes.Clear();
            ExternalDocumentTypes.Add("Все документы");
            var externalDocumentTypes = Enum.GetValues(typeof(ExternalDocumentType));
            foreach (var type in externalDocumentTypes)
            {
                ExternalDocumentTypes.Add(ExternalDocumentTypeConverter.ConvertToString(type));
            }
            SelectedExternalDocumentTypeIndex = 0;
            SelectedExternalDocumentType = ExternalDocumentTypes.FirstOrDefault();
        }

        private void InitializeInternalDocumentTypes()
        {
            InternalDocumentTypes.Clear();
            InternalDocumentTypes.Add("Все документы");
            var internalDocumentTypes = Enum.GetValues(typeof(InternalDocumentType));
            foreach (var type in internalDocumentTypes)
            {
                InternalDocumentTypes.Add(InternalDocumentTypeConverter.ConvertToString(type));
            }
            SelectedInternalDocumentTypeIndex = 0;
            selectedInternalDocumentType = InternalDocumentTypes.FirstOrDefault();
        }

        private void GetExternalDocuments()
        {
            ExternalDocumentsList.Clear();
            ExternalDocumentController documentController = new ExternalDocumentController();
            ExternalDocumentsList = documentController.GetAllDocuments();
            ExternalDocumentsList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
        }

        private void GetInternalDocuments() 
        {
            InternalDocumentsList.Clear();
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            InternalDocumentsList = internalDocumentController.GetInternalDocuments();
            InternalDocumentsList.Sort((d1, d2) => d2.RegistrationDate.CompareTo(d1.RegistrationDate));
        }

        private void GetEmployees()
        {
            Employees.Clear();
            EmployeeController employeeController = new EmployeeController();
            Employees = employeeController.GetAllEmployees();
        }

        private void GetDepartments()
        {
            Departments.Clear();
            DepartmentController departmentController = new DepartmentController();
            Departments = departmentController.GetAllDepartments();
        }

        private void GetContractorCompanies()
        {
            ContractorCompanies.Clear();
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            ContractorCompanies = contractorCompanyController.GetContractorCompanies();
        }
        private void InitializeExternalDocuments()
        {
            ExternalDocuments.Clear();
            if (ExternalDocumentsList != null)
            {
                foreach(ExternalDocument document in ExternalDocumentsList)
                {
                    ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                    if(contractorCompany != null) document.ContractorCompany = contractorCompany;
                    Employee employee = Employees.Where(e => e.EmployeeID == document.EmployeeID).FirstOrDefault();
                    if(employee != null) document.EmployeeReceivedDocument = employee;
                    ExternalDocuments.Add(document);
                }
            }
        }
        private void InitializeInternalDocuments()
        {
            InternalDocuments.Clear();
            if(InternalDocumentsList != null )
            {
                foreach (InternalDocument internalDocument in InternalDocumentsList) 
                {
                    Employee signatory = Employees.Where(s => s.EmployeeID == internalDocument.SignatoryID).FirstOrDefault();
                    if (signatory != null)
                    {
                        Department department = Departments.Where(d => d.DepartmentID == signatory.DepartmentID).FirstOrDefault();
                        if (department != null) signatory.Department = department;
                        Employee approvedManager = Employees.Where(a => a.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                        if (approvedManager != null)
                        {
                            department = Departments.Where(d => d.DepartmentID == approvedManager.DepartmentID).FirstOrDefault();
                            approvedManager.Department = department;
                        }
                        Employee employeeRecievedDocument = Employees.Where(r => r.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                        if (employeeRecievedDocument != null)
                        {
                            department = Departments.Where(d => d.DepartmentID == employeeRecievedDocument.DepartmentID).FirstOrDefault();
                            employeeRecievedDocument.Department = department;
                        }
                        internalDocument.Signatory = signatory;
                        internalDocument.ApprovedManager = approvedManager;
                        internalDocument.EmployeeRecievedDocument = employeeRecievedDocument;
                        InternalDocuments.Add(internalDocument);
                    }
                }
            }
        }

        private void GetExternalDocumentByDocumentStatus()
        {
            ExternalDocuments.Clear();
            List<ExternalDocument> externalDocuments = new List<ExternalDocument>();
            if (ExternalDocumentsList.Count > 0)
            {
                if (selectedExternalDocumentStatus == "Все входящие документы")
                {
                    externalDocuments = ExternalDocumentsList;
                }
                else
                {
                    DocumentStatus documentStatus = DocumentStatusConverter.ConvertToEnum(selectedExternalDocumentStatus);
                    externalDocuments = ExternalDocumentsList.Where(d => d.ExternalDocumentStatus == documentStatus).ToList();
                }
            }
            if (externalDocuments.Count > 0)
            {
                foreach (ExternalDocument externalDocument in externalDocuments)
                {
                    ExternalDocuments.Add(externalDocument);
                }
            }
        }

        private void GetInternalDocumentByDocumentStatus()
        {
            InternalDocuments.Clear();
            List<InternalDocument> internalDocuments = new List<InternalDocument>();
            if(InternalDocumentsList.Count > 0) {
                if (selectedInternalDocumentStatus == "Все внутренние документы")
                {
                    internalDocuments = InternalDocumentsList;
                }
                else
                {
                    DocumentStatus documentStatus = DocumentStatusConverter.ConvertToEnum(selectedInternalDocumentStatus);
                    internalDocuments = InternalDocumentsList.Where(d => d.InternalDocumentStatus == documentStatus).ToList();
                }
            }
            if(internalDocuments.Count > 0)
            {
                foreach(InternalDocument internalDocument in internalDocuments)
                {
                    InternalDocuments.Add(internalDocument);
                }
            }
        }

        private void GetExternalDocumentsByDocumentType()
        {
            GetExternalDocumentByDocumentStatus();
            List<ExternalDocument> externalDocuments = ExternalDocuments.ToList();
            ExternalDocuments.Clear();
            if (selectedExternalDocumentType == "Все документы")
            {
                foreach (ExternalDocument externalDocument in externalDocuments)
                {
                    ExternalDocuments.Add(externalDocument);
                }
                return;
            }
            else
            {
                ExternalDocumentType documentType = ExternalDocumentTypeConverter.ConvertToEnum(selectedExternalDocumentType);
                foreach (ExternalDocument externalDocument in externalDocuments)
                {
                    if (externalDocument.ExternalDocumentType == documentType)
                    {
                        ExternalDocuments.Add(externalDocument);
                    }
                }
            }
        }

        private void GetInternalDocumentsByDocumentType()
        {
            GetInternalDocumentByDocumentStatus();
            List<InternalDocument> internalDocuments = InternalDocuments.ToList();
            InternalDocuments.Clear();
            if (selectedInternalDocumentType == "Все документы")
            {
                foreach (InternalDocument internalDocument in internalDocuments)
                {
                    InternalDocuments.Add(internalDocument);
                }
                return;
            }
            else
            {
                InternalDocumentType documentType = InternalDocumentTypeConverter.ConvertToEnum(selectedInternalDocumentType);
                foreach (InternalDocument internalDocument in internalDocuments)
                {
                    if (internalDocument.InternalDocumentType == documentType)
                    {
                        InternalDocuments.Add(internalDocument);
                    }
                }
            }
        }

        public void GetDocumentBySearchString(string searchingString)
        {
            if (!IsInternalDocument)
            {
                if (string.IsNullOrEmpty(searchingString))
                {
                    GetExternalDocumentsByDocumentType();
                    return;
                }
                GetExternalDocumentsByDocumentType();
                List<ExternalDocument> documents = ExternalDocuments.ToList();
                ExternalDocuments.Clear();
                if (documents != null)
                {
                    documents.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                    foreach (ExternalDocument document in documents)
                    {
                        Employee employee = Employees.Where(e => e.EmployeeID == document.EmployeeID).FirstOrDefault();
                        ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                        document.EmployeeReceivedDocument = employee;
                        document.ContractorCompany = contractorCompany;
                        if (document.ExternalDocumentTitle.ToLower().Contains(searchingString.ToLower()))
                        {
                            ExternalDocuments.Add(document);
                        }
                    }
                    if (ExternalDocuments.Count == 0)
                    {
                        foreach (ExternalDocument document in documents)
                        {
                            if (document.ContractorCompany.ContractorCompanyTitle.ToLower().Contains(searchingString.ToLower()))
                            {
                                ExternalDocuments.Add(document);
                            }
                        }
                    }
                    if (ExternalDocuments.Count == 0)
                    {
                        foreach (ExternalDocument document in documents)
                        {
                            if (document.ExternalDocumentNumber != null && document.ExternalDocumentNumber.ToLower().Contains(searchingString.ToLower()))
                            {
                                ExternalDocuments.Add(document);
                            }
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(searchingString))
                {
                    GetInternalDocumentsByDocumentType();
                    return;
                }
                GetInternalDocumentsByDocumentType();
                List<InternalDocument> internalDocuments = InternalDocuments.ToList();
                InternalDocuments.Clear();
                if (internalDocuments != null)
                {
                    internalDocuments.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                    foreach (InternalDocument internalDocument in internalDocuments)
                    {
                        Employee signatory = Employees.Where(e => e.EmployeeID == internalDocument.SignatoryID).FirstOrDefault();
                        internalDocument.Signatory = signatory;
                        Employee approvedManager = Employees.Where(e => e.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                        internalDocument.ApprovedManager = approvedManager;
                        Employee employeeRecivedDocument = Employees.Where(e => e.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                        internalDocument.EmployeeRecievedDocument = employeeRecivedDocument;
                        if (internalDocument.Signatory.EmployeeFullName.ToLower().Contains(searchingString.ToLower()))
                        {
                            InternalDocuments.Add(internalDocument);
                        }
                        if (internalDocument.InternalDocumentContent.ToLower().Contains(searchingString.ToLower()))
                        {
                            InternalDocuments.Add(internalDocument);
                        }
                    }
                }
            }
        }

        public ICommand IShowExternalDocuments => new RelayCommand(showExternalDocuments => ShowExternalDocuments());
        private void ShowExternalDocuments()
        {
            ManagerPanelWindow.ComboExternalDocumentStatus.Visibility = System.Windows.Visibility.Visible;
            ManagerPanelWindow.ComboInternalDocumentStatus.Visibility = System.Windows.Visibility.Hidden;
            ManagerPanelWindow.NewInternalDocument.Visibility = System.Windows.Visibility.Hidden;
            ManagerPanelWindow.ComboBoxExternalDocumentTypes.Visibility = System.Windows.Visibility.Visible;
            ManagerPanelWindow.ComboBoxInternalDocumentTypes.Visibility = System.Windows.Visibility.Hidden;
            ManagerPanelWindow.ExternalDocuments.Visibility = System.Windows.Visibility.Visible;
            ManagerPanelWindow.InternalDocuments.Visibility = System.Windows.Visibility.Hidden;
            IsInternalDocument = false;
            SearchString = string.Empty;
            SearchStringContent = "Поиск по наименованию документа или наименованию контрагента...";
        }

        public ICommand IShowInternalDocuments => new RelayCommand(showInternalDocuments => ShowInternalDocuments());
        private void ShowInternalDocuments()
        {
            ManagerPanelWindow.ComboExternalDocumentStatus.Visibility = System.Windows.Visibility.Hidden;
            ManagerPanelWindow.ComboInternalDocumentStatus.Visibility = System.Windows.Visibility.Visible;
            ManagerPanelWindow.NewInternalDocument.Visibility = System.Windows.Visibility.Visible;
            ManagerPanelWindow.ComboBoxExternalDocumentTypes.Visibility = System.Windows.Visibility.Hidden;
            ManagerPanelWindow.ComboBoxInternalDocumentTypes.Visibility = System.Windows.Visibility.Visible;
            ManagerPanelWindow.ExternalDocuments.Visibility = System.Windows.Visibility.Hidden;
            ManagerPanelWindow.InternalDocuments.Visibility = System.Windows.Visibility.Visible;
            IsInternalDocument = true;
            SearchString = string.Empty;
            SearchStringContent = "Поиск по фамилии, имени, отчеству инициатора/подписанта, либо содержанию документа...";
        }

        public ICommand ICreateNewInternalDocument => new RelayCommand(createNewInternalDocument => CreateNewInternalDocument());
        private void CreateNewInternalDocument()
        {
            CreatingInternalDocumentWindow creatingInternalDocumentWindow = new CreatingInternalDocumentWindow();
            creatingInternalDocumentWindow.Show();
        }
    }
}