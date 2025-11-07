using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private EmployeeViewModel viewModel;
        public EmployeeWindow(Employee inputEmployee)
        {
            if (inputEmployee == null) viewModel = new EmployeeViewModel(this);
            else viewModel = new EmployeeViewModel(this, inputEmployee);
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
