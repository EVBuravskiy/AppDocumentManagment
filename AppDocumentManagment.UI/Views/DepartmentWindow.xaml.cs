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
    /// Логика взаимодействия для DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        private Department _selecedDepartment = null;
        private DepartmentViewModel _viewModel;

        public DepartmentWindow(Department inputDepartment)
        {
            _selecedDepartment = inputDepartment;
            InitializeComponent();
            _viewModel = new DepartmentViewModel(this, inputDepartment);
            DataContext = _viewModel;
        }
    }
}
