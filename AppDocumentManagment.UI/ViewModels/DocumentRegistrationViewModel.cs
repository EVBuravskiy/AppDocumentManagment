using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;


namespace AppDocumentManagment.UI.ViewModels
{
    public class DocumentRegistrationViewModel : BaseViewModelClass
    {
        private DocumentRegistrationWindow DocumentRegistrationWindow;
        private List<Employee> Employees;
        private List<ContractorCompany> ContractorCompanies;
        private List<Department> Departments;

        private List<ExtermalDocument> DocumentList;
        public ObservableCollection<ExtermalDocument> DocumentsCollection { get; set; }
        private ExtermalDocument selectedDocument;
        public ExtermalDocument SelectedDocument
        {
            get => selectedDocument;
            set
            {
                selectedDocument = value;
                OnPropertyChanged(nameof(SelectedDocument));
                if (SelectedDocument != null)
                {
                    OpenDocumentWindow(SelectedDocument);
                }
            }
        }

        public ObservableCollection<string> DocumentTypes { get; private set; }

        private int selectedDocumentTypeIndex;
        public int SelectedDocumentTypeIndex
        {
            get => selectedDocumentTypeIndex;
            set
            {
                selectedDocumentTypeIndex = value;
                OnPropertyChanged(nameof(SelectedDocumentTypeIndex));
            }
        }

        private string selectedDocumentType;
        public string SelectedDocumentType
        {
            get => selectedDocumentType;
            set
            {
                selectedDocumentType = value;
                OnPropertyChanged(nameof(SelectedDocumentType));
                int index = 0;
                for (; index < DocumentTypes.Count; index++)
                {
                    if (value.Equals(DocumentTypes[index]))
                    {
                        SelectedDocumentTypeIndex = index;
                        break;
                    }
                }
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                    else
                    {
                        GetDocumentsByDocumentType();
                    }
                }
            }
        }


        private List<InternalDocument> InternalDocumentList;
        public ObservableCollection<InternalDocument> InternalDocumentsCollection { get; set; }

        private InternalDocument selectedInternalDocument;
        public InternalDocument SelectedInternalDocument
        {
            get => selectedInternalDocument;
            set
            {
                selectedInternalDocument = value;
                OnPropertyChanged(nameof(SelectedInternalDocument));
                if (SelectedInternalDocument != null)
                {
                    OpenInternalDocumentWindow(SelectedInternalDocument);
                }
            }
        }

        public ObservableCollection<string> InternalDocumentRegistationStatus { get; set; }

        private int selectedInternalDocumentRegistationStatusIndex;
        public int SelectedInternalDocumentRegistationStatusIndex
        {
            get => selectedInternalDocumentRegistationStatusIndex;
            set
            {
                selectedInternalDocumentRegistationStatusIndex = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentRegistationStatusIndex));
            }
        }
        private string selectedInternalDocumentRegistationStatus;

        public string SelectedInternalDocumentRegistationStatus
        {
            get => selectedInternalDocumentRegistationStatus;
            set
            {
                selectedInternalDocumentRegistationStatus = value;
                OnPropertyChanged(nameof(SelectedInternalDocumentRegistationStatus));
                int index = 0;
                for (; index < InternalDocumentRegistationStatus.Count; index++)
                {
                    if (value.Equals(InternalDocumentRegistationStatus[index]))
                    {
                        SelectedInternalDocumentRegistationStatusIndex = index;
                        break;
                    }
                }
                if (value != null)
                {
                         if (!string.IsNullOrEmpty(SearchString))
                        {
                            GetDocumentBySearchString(SearchString);
                        }
                        else
                        {
                            GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
                        }
                }
            }
        }

        public ObservableCollection<string> InternalDocumentTypes { get; private set; }

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
                int index = 0;
                for (; index < InternalDocumentTypes.Count; index++)
                {
                    if (value.Equals(InternalDocumentTypes[index]))
                    {
                        SelectedInternalDocumentTypeIndex = index;
                        break;
                    }
                }
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        GetDocumentBySearchString(SearchString);
                    }
                    else
                    {
                        GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
                    }
                }
            }
        }



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

        private bool IsInternalDocuments = false;

        private string textBlockTitle = "Зарегистрированные поступившие документы";
        public string TextBlockTitle
        {
            get => textBlockTitle;
            set
            {
                textBlockTitle = value;
                OnPropertyChanged(nameof(TextBlockTitle));
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
        public DocumentRegistrationViewModel(DocumentRegistrationWindow window)
        {
            DocumentRegistrationWindow = window;
            Employees = new List<Employee>();
            ContractorCompanies = new List<ContractorCompany>();
            Departments = new List<Department>();
            DocumentList = new List<ExtermalDocument>();
            DocumentsCollection = new ObservableCollection<ExtermalDocument>();
            InternalDocumentRegistationStatus = new ObservableCollection<string>();
            DocumentTypes = new ObservableCollection<string>();
            InternalDocumentList = new List<InternalDocument>();
            InternalDocumentsCollection = new ObservableCollection<InternalDocument>();
            InternalDocumentTypes = new ObservableCollection<string>();
            InitializeDocumentRegistrationStatus();
            GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
            InitializeDocumentTypes();
            InitializeInternalDocumentTypes();
            GetAllDepartments();
            GetAllEmployees();
            GetAllContractorCompanyes();
            GetAllDocuments();
            InitializeDocuments();
            GetAllInternalDocuments();
            InitializeInternalDocuments();
        }

        private void InitializeDocumentRegistrationStatus()
        {
            InternalDocumentRegistationStatus = new ObservableCollection<string>
            {
                "Все внутренние документы",
                "Незарегистрированные внутренние документы",
                "Зарегистрированные внутренние документы"
            };
            SelectedInternalDocumentRegistationStatusIndex = 0;
            SelectedInternalDocumentRegistationStatus = InternalDocumentRegistationStatus.FirstOrDefault();
        }
        private void InitializeDocumentTypes()
        {
            DocumentTypes.Clear();
            DocumentTypes.Add("Все документы");
            var documentTypes = Enum.GetValues(typeof(ExternalDocumentType));
            foreach (var type in documentTypes)
            {
                DocumentTypes.Add(ExternalDocumentTypeConverter.ConvertToString(type));
            }
            SelectedDocumentType = DocumentTypes.FirstOrDefault();
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
            SelectedInternalDocumentType = InternalDocumentTypes.FirstOrDefault();
        }

        private void GetAllEmployees()
        {
            Employees.Clear();
            EmployeeController employeeController = new EmployeeController();
            Employees = employeeController.GetAllEmployees();
            if (Departments.Count > 0)
            {
                foreach (Employee employee in Employees)
                {
                    Department department = Departments.Where(d => d.DepartmentID == employee.DepartmentID).FirstOrDefault();
                    employee.Department = department;
                }
            }
        }

        private void GetAllContractorCompanyes()
        {
            ContractorCompanies.Clear();
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            ContractorCompanies = contractorCompanyController.GetContractorCompanies();
        }

        private void GetAllDepartments()
        {
            Departments.Clear();
            DepartmentController departmentController = new DepartmentController();
            Departments = departmentController.GetAllDepartments();
        }

        private void GetAllDocuments()
        {
            DocumentList.Clear();
            ExternalDocumentController documentController = new ExternalDocumentController();
            DocumentList = documentController.GetAllDocuments();
        }


        private void InitializeDocuments()
        {
            DocumentsCollection.Clear();
            if (DocumentList != null)
            {
                DocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                foreach (ExtermalDocument document in DocumentList)
                {
                    Employee employee = Employees.Where(e => e.EmployeeID == document.EmployeeID).FirstOrDefault();
                    ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                    document.EmployeeReceivedDocument = employee;
                    document.ContractorCompany = contractorCompany;
                    DocumentsCollection.Add(document);
                }
            }
        }

        private void GetAllInternalDocuments()
        {
            InternalDocumentList.Clear();
            InternalDocumentController internalDocumentController = new InternalDocumentController();
            InternalDocumentList = internalDocumentController.GetInternalDocuments();
        }


        private void InitializeInternalDocuments()
        {
            InternalDocumentsCollection.Clear();
            if (InternalDocumentList != null)
            {
                InternalDocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                foreach (InternalDocument internalDocument in InternalDocumentList)
                {
                    Employee signatory = Employees.Where(e => e.EmployeeID == internalDocument.SignatoryID).FirstOrDefault();
                    internalDocument.Signatory = signatory;
                    Employee approvedManager = Employees.Where(e => e.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                    internalDocument.ApprovedManager = approvedManager;
                    Employee employeeRecivedDocument = Employees.Where(e => e.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                    internalDocument.EmployeeRecievedDocument = employeeRecivedDocument;
                    InternalDocumentsCollection.Add(internalDocument);
                }
            }
        }

        public ICommand IShowExternalDocuments => new RelayCommand(showExternalDocuments => ShowExternalDocuments());
        private void ShowExternalDocuments()
        {
            DocumentRegistrationWindow.ExternalDocuments.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.InternalDocuments.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.ComboBoxExternalDocumentTypes.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.ComboBoxInternalDocumentTypes.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.ExternalDocumentTitle.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.SelectInternalDocumentStatus.Visibility = System.Windows.Visibility.Hidden;
            IsInternalDocuments = false;
            SearchString = string.Empty;
            SelectedInternalDocumentType = "Все документы";
            GetDocumentsByDocumentType();
            SearchStringContent = "Поиск по наименованию документа или наименованию контрагента...";
        }

        public ICommand IShowInternalDocuments => new RelayCommand(showInternalDocuments => ShowInternalDocuments());
        private void ShowInternalDocuments()
        {
            DocumentRegistrationWindow.ExternalDocuments.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.InternalDocuments.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.ComboBoxExternalDocumentTypes.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.ComboBoxInternalDocumentTypes.Visibility = System.Windows.Visibility.Visible;
            DocumentRegistrationWindow.ExternalDocumentTitle.Visibility = System.Windows.Visibility.Hidden;
            DocumentRegistrationWindow.SelectInternalDocumentStatus.Visibility = System.Windows.Visibility.Visible;
            IsInternalDocuments = true;
            SearchString = string.Empty;
            SelectedDocumentType = "Все документы";
            GetDocumentsByDocumentType();
            SearchStringContent = "Поиск по инициатору/подписанту документа...";
            SelectedInternalDocumentRegistationStatus = InternalDocumentRegistationStatus.FirstOrDefault();
            //GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
        }


        private void GetDocumentsByDocumentType()
        {
            if (!IsInternalDocuments)
            {
                if (SelectedDocumentType.Equals("Все документы"))
                {
                    InitializeDocuments();
                }
                else
                {
                    DocumentsCollection.Clear();
                    if (DocumentList != null)
                    {
                        DocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                        foreach (ExtermalDocument document in DocumentList)
                        {
                            ExternalDocumentType documentType = ExternalDocumentTypeConverter.ConvertToEnum(SelectedDocumentType);
                            if (document.DocumentType == documentType)
                            {
                                Employee employee = Employees.Where(e => e.EmployeeID == document.EmployeeID).FirstOrDefault();
                                ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();
                                document.EmployeeReceivedDocument = employee;
                                document.ContractorCompany = contractorCompany;
                                DocumentsCollection.Add(document);
                            }
                        }
                    }
                }
            }
            else
            {
                if (SelectedInternalDocumentType.Equals("Все документы"))
                {
                    InitializeInternalDocuments();
                }
                else
                {
                    InternalDocumentsCollection.Clear();
                    if (InternalDocumentList != null)
                    {
                        InternalDocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                        foreach (InternalDocument internalDocument in InternalDocumentList)
                        {
                            InternalDocumentTypes internalDocumentType = InternalDocumentTypeConverter.ConvertToEnum(SelectedInternalDocumentType);
                            if (internalDocument.InternalDocumentType == internalDocumentType)
                            {
                                Employee signatory = Employees.Where(e => e.EmployeeID == internalDocument.SignatoryID).FirstOrDefault();
                                internalDocument.Signatory = signatory;
                                Employee approvedManager = Employees.Where(e => e.EmployeeID == internalDocument.ApprovedManagerID).FirstOrDefault();
                                internalDocument.ApprovedManager = approvedManager;
                                Employee employeeRecivedDocument = Employees.Where(e => e.EmployeeID == internalDocument.EmployeeRecievedDocumentID).FirstOrDefault();
                                internalDocument.EmployeeRecievedDocument = employeeRecivedDocument;
                                InternalDocumentsCollection.Add(internalDocument);
                            }
                        }
                    }
                }
            }
        }
        private void GetDocumentsByRegistrationStatus(string selectedInternalDocumentRegistationStatus)
        {
            if (!IsInternalDocuments) return;
            if(SelectedInternalDocumentRegistationStatus.Equals("Все внутренние документы"))
            {
                GetDocumentsByDocumentType();
            }
            else
            {
                GetDocumentsByDocumentType();
                List<InternalDocument> internalDocuments = new List<InternalDocument>();
                if (SelectedInternalDocumentRegistationStatus.Equals("Незарегистрированные внутренние документы")) {
                    internalDocuments = InternalDocumentsCollection.Where(r => r.IsRegistated == false).ToList();
                    InternalDocumentsCollection.Clear();
                    foreach (InternalDocument document in internalDocuments)
                    {
                        InternalDocumentsCollection.Add(document);
                    }
                }
                else
                {
                    internalDocuments = InternalDocumentsCollection.Where(r => r.IsRegistated == true).ToList();
                    InternalDocumentsCollection.Clear();
                    foreach (InternalDocument document in internalDocuments)
                    {
                        InternalDocumentsCollection.Add(document);
                    }
                }
            }
        }

        public void GetDocumentBySearchString(string searchingString)
        {
            if (!IsInternalDocuments)
            {
                if (string.IsNullOrEmpty(searchingString))
                {
                    DocumentsCollection.Clear();
                    GetDocumentsByDocumentType();
                    return;
                }
                GetDocumentsByDocumentType();
                List<ExtermalDocument> documents = DocumentsCollection.ToList();
                DocumentsCollection.Clear();
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
                            DocumentsCollection.Add(document);
                        }
                    }
                    if (DocumentsCollection.Count == 0)
                    {
                        foreach (ExtermalDocument document in documents)
                        {
                            if (document.ContractorCompany.ContractorCompanyTitle.ToLower().Contains(searchingString.ToLower()))
                            {
                                DocumentsCollection.Add(document);
                            }
                        }
                    }
                    if (DocumentsCollection.Count == 0)
                    {
                        foreach (ExtermalDocument document in documents)
                        {
                            if (document.DocumentNumber != null && document.DocumentNumber.ToLower().Contains(searchingString.ToLower()))
                            {
                                DocumentsCollection.Add(document);
                            }
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(searchingString))
                {
                    GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
                    return;
                }
                GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
                List<InternalDocument> internalDocuments = InternalDocumentsCollection.ToList();
                InternalDocumentsCollection.Clear();
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
                            InternalDocumentsCollection.Add(internalDocument);
                        }
                    }
                }
            }
        }

        public ICommand ICreateNewDocument => new RelayCommand(createNewDocument => CreateNewDocument());
        private void CreateNewDocument()
        {
            if (!IsInternalDocuments)
            {
                OpenDocumentWindow(null);
            }
            else
            {
                OpenInternalDocumentWindow(null);
            }
        }

        private void OpenDocumentWindow(ExtermalDocument document)
        {
            DocumentWindow documentWindow = new DocumentWindow(document);
            documentWindow.ShowDialog();
            GetAllContractorCompanyes();
            GetAllDocuments();
            InitializeDocuments();
        }

        private void OpenInternalDocumentWindow(InternalDocument internalDocument)
        {
            InternalDocumentWindow internalDocumentWindow = new InternalDocumentWindow(internalDocument);
            internalDocumentWindow.ShowDialog();
            GetAllInternalDocuments();
            InitializeInternalDocuments();
            if (string.IsNullOrEmpty(SearchString))
            {
                GetDocumentsByRegistrationStatus(SelectedInternalDocumentRegistationStatus);
                return;
            }
            GetDocumentBySearchString(SearchString);
        }
    }
}
