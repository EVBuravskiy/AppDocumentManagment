using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class AdminPanelViewModel : BaseViewModelClass
    {
        private AdminPanelWindow _adminPanelWindow;

        public AdminPanelViewModel(AdminPanelWindow adminPanelWindow)
        {
            _adminPanelWindow = adminPanelWindow;
        }

        public ICommand IOpenPersonellRecordsWindow => new RelayCommand(openDepartment => OpenPersonnelRecordsWindow());

        private void OpenPersonnelRecordsWindow()
        {
            DepartmentsEmployeesPanelWindow departmentsEmployeesPanelWindow = new DepartmentsEmployeesPanelWindow();
            departmentsEmployeesPanelWindow.Show();
            _adminPanelWindow.Close();
        }

        public ICommand IOpenUserRegistrationWindow => new RelayCommand(openUserRegistrationWindow => OpenUserRegistrationWindow());
        private void OpenUserRegistrationWindow()
        {
            UserRegistrationWindow userRegistrationWindow = new UserRegistrationWindow();
            userRegistrationWindow.Show();
            _adminPanelWindow.Close();
        }

    }
}
