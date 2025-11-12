using AppDocumentManagment.UI.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
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
    /// Логика взаимодействия для InitialAuthorizationWindow.xaml
    /// </summary>
    public partial class InitialAuthorizationWindow : Window
    {
        private InitialAuthorizationViewModel viewModel;
        private bool visible = false;
        public InitialAuthorizationWindow()
        {
            viewModel = new InitialAuthorizationViewModel(this);
            InitializeComponent();
            DataContext = viewModel;
        }
        
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TextPasswordBox.Text = PasswordBox.Password;
            viewModel.Password = TextPasswordBox.Text;
        }

        private void ShowPass(object sender, RoutedEventArgs e)
        {
            if (visible == false)
            {
                PasswordBox.Visibility = Visibility.Hidden;
                TextPasswordBox.Visibility = Visibility.Visible;
                visible = true;
            }
            else
            {
                PasswordBox.Password = TextPasswordBox.Text;
                TextPasswordBox.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;
                visible = false;
            }
        }
    }
}
