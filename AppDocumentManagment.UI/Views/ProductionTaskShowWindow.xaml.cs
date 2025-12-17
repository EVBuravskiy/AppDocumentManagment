using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System.Windows;


namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductionTaskShowWindow.xaml
    /// </summary>
    public partial class ProductionTaskShowWindow : Window
    {
        private ProductionTaskShowViewModel viewModel;
        public ProductionTaskShowWindow(Employee currentEmployee, ProductionTask currentProductionTask)
        {
            InitializeComponent();
            viewModel = new ProductionTaskShowViewModel(this, currentEmployee, currentProductionTask);
            DataContext = viewModel;
        }
    }
}
