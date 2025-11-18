using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для InternalDocumentShowWindow.xaml
    /// </summary>
    public partial class InternalDocumentShowWindow : Window
    {
        private InternalDocumentShowViewModel viewModel;

        public InternalDocumentShowWindow(InternalDocument internalDocument, int currentEmployeeID)
        {
            InitializeComponent();
            viewModel = new InternalDocumentShowViewModel(this, internalDocument, currentEmployeeID);
            DataContext = viewModel;
        }
    }
}
