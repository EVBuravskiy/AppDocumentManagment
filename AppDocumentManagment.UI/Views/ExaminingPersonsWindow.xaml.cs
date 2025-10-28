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
    /// Логика взаимодействия для ExaminingPersonsWindow.xaml
    /// </summary>
    public partial class ExaminingPersonsWindow : Window
    {
        private ExaminingPersonViewModel examiningPersonViewModel;
        public ExaminingPersonsWindow()
        {
            examiningPersonViewModel = new ExaminingPersonViewModel(this);
            InitializeComponent();
            DataContext = examiningPersonViewModel;
        }
    }
}
