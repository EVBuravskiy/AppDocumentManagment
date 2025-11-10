using AppDocumentManagment.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;


namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ManagerPanelWindow.xaml
    /// </summary>
    public partial class ManagerPanelWindow : Window
    {
        private ManagerPanelViewModel viewModel;
        public ManagerPanelWindow()
        {
            InitializeComponent();
            viewModel = new ManagerPanelViewModel(this);
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
