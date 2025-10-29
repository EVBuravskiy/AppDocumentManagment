using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace AppDocumentManagment.UI.ViewModels
{
    public class DocumentRegistrationViewModel : BaseViewModelClass
    {
        private DocumentRegistrationWindow DocumentRegistrationWindow;
        private List<Document> DocumentList;
        private List<Employee> Employees;
        private List<ContractorCompany> ContractorCompanies;
        public ObservableCollection<Document> DocumentsCollection {  get; set; }
        private Document selectedDocument;
        public Document SelectedDocument
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
            DocumentList = new List<Document>();
            Employees = new List<Employee>();
            ContractorCompanies = new List<ContractorCompany>();
            DocumentsCollection = new ObservableCollection<Document>();
            DocumentTypes = new ObservableCollection<string>();
            GetAllDocuments();
            GetAllEmployees();
            GetAllContractorCompanyes();
            InitializeDocumentTypes();
            InitializeDocuments();
        }

        private void GetAllDocuments()
        {
            DocumentList.Clear();
            DocumentController documentController = new DocumentController();
            DocumentList = documentController.GetAllDocuments();
        }

        private void GetAllEmployees()
        {
            Employees.Clear();
            EmployeeController employeeController = new EmployeeController();
            Employees = employeeController.GetAllEmployees();
        }

        private void GetAllContractorCompanyes()
        {
            ContractorCompanies.Clear();
            ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
            ContractorCompanies = contractorCompanyController.GetContractorCompanies();
        }

        private void InitializeDocuments()
        {
            DocumentsCollection.Clear();
            if (DocumentList != null)
            {
                DocumentList.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                foreach (Document document in DocumentList)
                {
                    Employee employee = Employees.Where(e => e.EmployeeID == document.EmployeeID).FirstOrDefault();
                    ContractorCompany contractorCompany = ContractorCompanies.Where(c => c.ContractorCompanyID == document.ContractorCompanyID).FirstOrDefault();   
                    document.EmployeeReceivedDocument = employee;
                    document.ContractorCompany = contractorCompany;
                    DocumentsCollection.Add(document);
                }
            }
        }

        private void InitializeDocumentTypes()
        {
            DocumentTypes.Clear();
            DocumentTypes.Add("Все документы");
            var documentTypes = Enum.GetValues(typeof(DocumentType));
            foreach (var type in documentTypes)
            {
                DocumentTypes.Add(DocumentTypeConverter.ConvertToString(type));
            }
            SelectedDocumentType = DocumentTypes.FirstOrDefault();
        }

        private void GetDocumentsByDocumentType()
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
                    foreach (Document document in DocumentList)
                    {
                        DocumentType documentType = DocumentTypeConverter.ConvertToEnum(SelectedDocumentType);
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

        public void GetDocumentBySearchString(string searchingString)
        {
            if(string.IsNullOrEmpty(searchingString))
            {
                DocumentsCollection.Clear();
                GetDocumentsByDocumentType();
                return;
            }
            GetDocumentsByDocumentType();
            List<Document> documents = DocumentsCollection.ToList();
            DocumentsCollection.Clear();
            if (documents != null)
            {
                documents.Sort((d1, d2) => d1.RegistrationDate.CompareTo(d2.RegistrationDate));
                foreach (Document document in documents)
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
                if(DocumentsCollection.Count == 0)
                {
                    foreach (Document document in documents)
                    {
                        if (document.ContractorCompany.ContractorCompanyTitle.ToLower().Contains(searchingString.ToLower()))
                        {
                            DocumentsCollection.Add(document);
                        }
                    }
                }
                if (DocumentsCollection.Count == 0)
                {
                    foreach (Document document in documents)
                    {
                        if (document.DocumentNumber != null && document.DocumentNumber.ToLower().Contains(searchingString.ToLower()))
                        {
                            DocumentsCollection.Add(document);
                        }
                    }
                }
            }
        }

        public ICommand ICreateNewDocument => new RelayCommand(createNewDocument => CreateNewDocument());
        private void CreateNewDocument()
        {
            OpenDocumentWindow(null);
        }

        private void OpenDocumentWindow(Document document)
        {
            DocumentWindow documentWindow = new DocumentWindow(document);
            documentWindow.ShowDialog();
            GetAllDocuments();
            GetAllContractorCompanyes();
            InitializeDocuments();
        }

    }
}
