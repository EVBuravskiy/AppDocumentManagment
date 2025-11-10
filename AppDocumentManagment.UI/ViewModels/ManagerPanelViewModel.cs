using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using Microsoft.Identity.Client;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Linq;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ManagerPanelViewModel : BaseViewModelClass
    {
        private ManagerPanelWindow ManagerPanelWindow { get; set; }

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
            }
        }

        private List<ExtermalDocument> ExternalDocumentsList;
        public ObservableCollection<ExtermalDocument> ExternalDocuments { get; set; }

        private ExtermalDocument selectedExternalDocument;
        public ExtermalDocument SelectedExternalDocument
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

        private bool IsInternalDocument = false;

        public ManagerPanelViewModel(ManagerPanelWindow window)
        {
            ManagerPanelWindow = window;
            ExternalDocumentsList = new List<ExtermalDocument>();
            InternalDocumentsList = new List<InternalDocument>();
            Employees = new List<Employee>();
            Departments = new List<Department>();
            ContractorCompanies = new List<ContractorCompany>();
            ExternalDocumentStatus = new ObservableCollection<string>();
            InternalDocumentStatus = new ObservableCollection<string>();
            ExternalDocumentTypes = new ObservableCollection<string>();
            InternalDocumentTypes = new ObservableCollection<string>();
            ExternalDocuments = new ObservableCollection<ExtermalDocument>();
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

        private void InitializeExternalDocumentStatus()
        {
            ExternalDocumentStatus.Clear();
            ExternalDocumentStatus = new ObservableCollection<string>
                {
                    "Все входящие документы",
                    "Рассмотренные входящие документы",
                    "Входящие документы на рассмотрении"
                };
            SelectedExternalDocumentStatusIndex = 0;
            SelectedExternalDocumentStatus = ExternalDocumentStatus.FirstOrDefault();
        }

        private void InitializeInternalDocumentStatus()
        {
            InternalDocumentStatus.Clear();
            InternalDocumentStatus = new ObservableCollection<string>
                {
                    "Все внутренние документы",
                    "Рассмотренные внутренние документы",
                    "Внутренние документы на рассмотрении"
                };
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
            var internalDocumentTypes = Enum.GetValues(typeof(InternalDocumentTypes));
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
        }

        private void GetInternalDocuments() 
        {
            InternalDocumentsList.Clear();
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            InternalDocumentsList = internalDocumentController.GetInternalDocuments();
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
                foreach(ExtermalDocument document in ExternalDocumentsList)
                {
                    ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                    document.ContractorCompany = contractorCompany;
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
                    Department department = Departments.Where(d => d.DepartmentID == signatory.DepartmentID).FirstOrDefault();
                    signatory.Department = department;
                    Employee approvedManager = Employees.Where(a => a.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                    department = Departments.Where(d => d.DepartmentID == approvedManager.DepartmentID).FirstOrDefault();
                    approvedManager.Department = department;
                    Employee employeeRecievedDocument = Employees.Where(r => r.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                    department = Departments.Where(d => d.DepartmentID == employeeRecievedDocument.DepartmentID).FirstOrDefault();
                    employeeRecievedDocument.Department = department;
                    internalDocument.Signatory = signatory;
                    internalDocument.ApprovedManager = approvedManager;
                    internalDocument.EmployeeRecievedDocument = employeeRecievedDocument;
                }
            }
        }

        private void GetExternalDocumentByDocumentStatus()
        {
            ExternalDocuments.Clear();
            if(selectedInternalDocumentType == "Все входящие документы")
            {
                if(ExternalDocumentsList.Count > 0)
                {
                    foreach(ExtermalDocument externalDocument in ExternalDocumentsList)
                    {
                        ExternalDocuments.Add(externalDocument);
                    }
                }
                return;
            }
            if(selectedInternalDocumentType == "Рассмотренные входящие документы")
            {
                if (ExternalDocumentsList.Count > 0)
                { 
                    foreach(ExtermalDocument externalDocument in ExternalDocumentsList)
                    {
                        
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
                    ExternalDocuments.Clear();
                    //GetDocumentsByDocumentType();
                    return;
                }
                //GetDocumentsByDocumentType();
                List<ExtermalDocument> documents = ExternalDocuments.ToList();
                ExternalDocuments.Clear();
                if (documents != null)
                {
                    documents.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                    foreach (ExtermalDocument document in documents)
                    {
                        Employee employee = Employees.Where(e => e.EmployeeID == document.EmployeeID).FirstOrDefault();
                        ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                        document.EmployeeReceivedDocument = employee;
                        document.ContractorCompany = contractorCompany;
                        if (document.DocumentTitle.ToLower().Contains(searchingString.ToLower()))
                        {
                            ExternalDocuments.Add(document);
                        }
                    }
                    if (ExternalDocuments.Count == 0)
                    {
                        foreach (ExtermalDocument document in documents)
                        {
                            if (document.ContractorCompany.ContractorCompanyTitle.ToLower().Contains(searchingString.ToLower()))
                            {
                                ExternalDocuments.Add(document);
                            }
                        }
                    }
                    if (ExternalDocuments.Count == 0)
                    {
                        foreach (ExtermalDocument document in documents)
                        {
                            if (document.DocumentNumber != null && document.DocumentNumber.ToLower().Contains(searchingString.ToLower()))
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
                    //GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
                    return;
                }
                //GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
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
        }
    }
}