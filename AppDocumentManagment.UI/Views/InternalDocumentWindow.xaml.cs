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
    /// Логика взаимодействия для InternalDocumentWindow.xaml
    /// </summary>
    public partial class InternalDocumentWindow : Window
    {
        private InternalDocumentViewModel viewModel;
        public InternalDocumentWindow()
        {
            viewModel = new InternalDocumentViewModel(this);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
