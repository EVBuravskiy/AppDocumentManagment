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
    /// Логика взаимодействия для DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {
        private DocumentViewModel viewController;
        public DocumentWindow(ExtermalDocument inputDocument)
        {
            InitializeComponent();
            viewController = new DocumentViewModel(this, inputDocument);
            DataContext = viewController;
        }
    }
}
