using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterUserWindow.xaml
    /// </summary>
    public partial class RegisterUserWindow : Window
    {
        private RegisterUserViewModel _viewModel;

        public RegisterUserWindow(Employee selectedEmployee, bool isRegistred)
        {
            InitializeComponent();
            _viewModel = new RegisterUserViewModel(this, selectedEmployee, isRegistred);
            DataContext = _viewModel;
        }

        private void EmployeeLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowLoginRequirements();
        }

        private void EmployeePassword_GotFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowPasswordRequirements();
        }

        private void EmployeeLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowDefaultRequirements();
        }

        private void EmployeePassword_LostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowDefaultRequirements();
        }
    }
}
