using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class DocumentShowViewModel : BaseViewModelClass
    {
        DocumentDisplayWindow DocumentShowWindow;

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

        public ObservableCollection<DocumentFile> DocumentFiles;

        private DocumentFile selectedDocumentFile;
        public DocumentFile SelectedDocumentFile
        {
            get => selectedDocumentFile;
            set
            {
                selectedDocumentFile = value;
                OnPropertyChanged(nameof(SelectedDocumentFile));
            }
        }

        public DocumentShowViewModel(DocumentDisplayWindow documentShowWindow, Document inputDocument)
        {
            DocumentShowWindow = documentShowWindow;
            DocumentFiles = new ObservableCollection<DocumentFile>();
            if (inputDocument != null)
            {
                DocumentType = DocumentTypeConverter.ConvertToString(inputDocument.DocumentType);
                DocumentNumber = inputDocument.DocumentNumber;
                DocumentTitle = inputDocument.DocumentTitle;
                ContractorCompanyController contractorCompanyController = new ContractorCompanyController();
                ContractorCompany contractorCompany = contractorCompanyController.GetContractorCompanyByID(inputDocument.ContractorCompanyID);
                if (contractorCompany != null)
                {
                    TextBlockCompanyTitle = contractorCompany.ContractorCompanyTitle;
                    TextBlockCompanyShortTitle = contractorCompany.ContractorCompanyShortTitle;
                    TextBlockCompanyAddress = contractorCompany.ContractorCompanyAddress;
                    TextBlockCompanyAddress = contractorCompany.ContractorCompanyAddress;
                    TextBlockCompanyPhone = contractorCompany.ContractorCompanyPhone;
                    TextBlockCompanyEmail = contractorCompany.ContractorCompanyEmail;
                }
                DocumentFileController documentFileController = new DocumentFileController();
                List<DocumentFile> documentFiles = new List<DocumentFile>();
                documentFiles = documentFileController.GetDocumentFiles(inputDocument.DocumentID);
                if (documentFiles.Count > 0)
                {
                    foreach (DocumentFile file in documentFiles)
                    {
                        DocumentFiles.Add(file);
                    }
                }
            }
        }

        public ICommand ILoadDocumentFiles;

        public ICommand IBrowseDocumentFiles;

        public ICommand ISendToWork;

        public ICommand ICreateTask;

        public ICommand IExit;
    }
}
