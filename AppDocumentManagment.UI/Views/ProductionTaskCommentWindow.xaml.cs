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
    /// Логика взаимодействия для ProductionTaskCommentWindow.xaml
    /// </summary>
    public partial class ProductionTaskCommentWindow : Window
    {
        public ProductionTaskCommentViewModel viewModel { get; set; }
        public ProductionTaskCommentWindow(ProductionTask currentProductionTask, Employee currentEmployee)
        {
            viewModel = new ProductionTaskCommentViewModel(this, currentProductionTask, currentEmployee);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
