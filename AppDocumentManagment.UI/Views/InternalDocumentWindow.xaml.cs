using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.ViewModels;
using System.Windows;

namespace AppDocumentManagment.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для InternalDocumentWindow.xaml
    /// </summary>
    public partial class InternalDocumentWindow : Window
    {
        private InternalDocumentViewModel viewModel;
        public InternalDocumentWindow(InternalDocument inputDocument)
        {
            InitializeComponent();
            viewModel = new InternalDocumentViewModel(this, inputDocument);
            DataContext = viewModel;
        }
    }
}
