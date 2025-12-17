using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System.Windows;


namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ExternalDocumentShowWindow.xaml
    /// </summary>
    public partial class ExternalDocumentShowWindow : Window
    {
        private ExternalDocumentShowViewModel viewModel;
        public ExternalDocumentShowWindow(ExternalDocument inputExternalDocument, ContractorCompany documentContractorCompany, Employee currentEmployee)
        {
            InitializeComponent();
            viewModel = new ExternalDocumentShowViewModel(this, inputExternalDocument, documentContractorCompany, currentEmployee);
            DataContext = viewModel;
        }
    }
}
