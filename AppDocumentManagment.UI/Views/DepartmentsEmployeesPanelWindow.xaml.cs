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
    /// Логика взаимодействия для DepartmentsEmployeesPanelWindow.xaml
    /// </summary>
    public partial class DepartmentsEmployeesPanelWindow : Window
    {
        private DepartmentsEmployeesPanelViewModel _departmentsEmployeesPanelViewModel;

        public DepartmentsEmployeesPanelWindow()
        {
            InitializeComponent();
            _departmentsEmployeesPanelViewModel = new DepartmentsEmployeesPanelViewModel(this);
            DataContext = _departmentsEmployeesPanelViewModel;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            _departmentsEmployeesPanelViewModel.SearchString = textBox.Text;
            _departmentsEmployeesPanelViewModel.FindItems(textBox.Text);
        }
    }
}
