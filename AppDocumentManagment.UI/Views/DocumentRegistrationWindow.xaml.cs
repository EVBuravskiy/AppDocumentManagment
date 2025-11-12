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
    /// Логика взаимодействия для DocumentRegistrationWindow.xaml
    /// </summary>
    public partial class DocumentRegistrationWindow : Window
    {
        private DocumentRegistrationViewModel viewModel;
        public DocumentRegistrationWindow(int currentUserID)
        {
            viewModel = new DocumentRegistrationViewModel(this, currentUserID);
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            viewModel.SearchString = textBox.Text;
            viewModel.GetDocumentBySearchString(textBox.Text);
        }
    }
}
