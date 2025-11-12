using AppDocumentManagment.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для CreatingInternalDocumentWindow.xaml
    /// </summary>
    public partial class CreatingInternalDocumentWindow : Window
    {
        private CreatingInternalDocumentViewModel viewModel;
        public CreatingInternalDocumentWindow(int currentUserID)
        {
            InitializeComponent();
            viewModel = new CreatingInternalDocumentViewModel(this, currentUserID);
            DataContext = viewModel; 
        }
    }
}
