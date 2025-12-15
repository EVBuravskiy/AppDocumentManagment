using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ExaminingPersonsWindow.xaml
    /// </summary>
    public partial class ExaminingPersonsWindow : Window
    {
        public ExaminingPersonViewModel viewModel;
        public ExaminingPersonsWindow(bool needManager, Department selectDepartment)
        {
            viewModel = new ExaminingPersonViewModel(this, needManager, selectDepartment);
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            viewModel.SearchString = textBox.Text;
            viewModel.GetEmployeeBySearchString(textBox.Text);
        }
    }
}
