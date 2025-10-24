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
                SelectedDocumentTypeIndex = DocumentTypeConverter.ToIntConvert(value);
                OnPropertyChanged(nameof(SelectedDocumentTypeIndex));
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

        public List<DocumentFile> DocumentFiles { get; set; }
        private DateTime RegistrationDate { get; set; }

    }
}
