using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System.Windows;


namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductionTaskWindow.xaml
    /// </summary>
    public partial class ProductionTaskWindow : Window
    {
        private ProductionTaskViewModel viewModel;

        public ProductionTaskWindow(Employee currentEmployee, ExternalDocument externalDocument, InternalDocument internalDocument)
        {
            InitializeComponent();
            viewModel = new ProductionTaskViewModel(this, currentEmployee, externalDocument, internalDocument);
            DataContext = viewModel;
        }
    }
}
