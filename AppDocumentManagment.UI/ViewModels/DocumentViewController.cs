using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;

namespace AppDocumentManagment.UI.ViewModels
{
    public class DocumentViewController : BaseViewModelClass
    {
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
                OnPropertyChanged(nameof(TextBlockCompanyTitle));
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

        public List<DocumentFile> DocumentFiles { get; set; }
        private DateTime RegistrationDate { get; set; }

        public DocumentViewController(DocumentWindow window, Document selectedDocument)
        {
            DocumentWindow = window;
            SelectedDocument = selectedDocument;
            if (SelectedDocument != null)
            {
                DocumentTitle = SelectedDocument.DocumentTitle;
                DocumentNumber = SelectedDocument.DocumentNumber;
                DocumentDate = SelectedDocument.DocumentDate;
                ContractorCompany = SelectedDocument.ContractorCompany;
                ContractorCompanyID = SelectedDocument.ContractorCompanyID;
                SelectedDocumentType = SelectedDocument.DocumentType;
                DocumentFiles = SelectedDocument.DocumentFiles;
            }
            else
            {
                DocumentDate = DateTime.Now;
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


    }
}
