using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.UI.ViewModels
{
    public class InternalDocumentViewModel : BaseViewModelClass
    {
        private InternalDocumentWindow InternalDocumentWindow;
        private IFileDialogService fileDialogService;
        public List<string> InternalDocumentTypes { get; set; }

        private DocumentType selectedDocumentType;

        public DocumentType SelectedDocumentType
        {
            get => selectedDocumentType;
            set
            {
                selectedDocumentType = value;
                OnPropertyChanged(nameof(SelectedDocumentType));
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

        private List<DocumentFile> DocumentFilesList { get; set; }

        public ObservableCollection<DocumentFile> DocumentFiles { get; set; }

        public DocumentFile SelectedDocumentFile { get; set; }

        public InternalDocumentViewModel(InternalDocumentWindow internalDocumentWindow)
        {
            InternalDocumentWindow = internalDocumentWindow;
            InternalDocumentTypes = new List<string>();
            DocumentFilesList = new List<DocumentFile>();
            DocumentFiles = new ObservableCollection<DocumentFile>();
        }

        private void InitializeInternalDocumentTypes()
        {
            InternalDocumentTypes = new List<string>();
            var internalDocumentTypes = Enum.GetValues(typeof(DocumentInternalType));
            foreach (var type in internalDocumentTypes)
            {
                InternalDocumentTypes.Add(DocumentTypeConverter.ConvertToString(type));
            }
        }
    }
}
