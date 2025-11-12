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
    /// Логика взаимодействия для UserRegistrationWindow.xaml
    /// </summary>
    public partial class UserRegistrationWindow : Window
    {
        private UserRegistrationViewModel _viewModel;
        public UserRegistrationWindow(int currentUserID)
        {
            _viewModel = new UserRegistrationViewModel(this, currentUserID);
            InitializeComponent();
            DataContext = _viewModel;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            _viewModel.SearchString = textBox.Text;
            _viewModel.FindUserBySearchString(textBox.Text);
        }
    }
}
