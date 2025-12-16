using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class AdminPanelViewModel : BaseViewModelClass
    {
        private AdminPanelWindow _adminPanelWindow;

        private Employee currentUser;

        private string greating;
        public string Greating
        {
            get => greating;
            set
            {
                greating = value;
                OnPropertyChanged(nameof(Greating));
            }
        }

        public AdminPanelViewModel(AdminPanelWindow adminPanelWindow, int currentUserID)
        {
            _adminPanelWindow = adminPanelWindow;
            InitializeCurrentUser(currentUserID);
        }

        private void InitializeCurrentUser(int currentUserID)
        {
            if (currentUserID == 0) return;
            EmployeeController employeeController = new EmployeeController();
            currentUser = employeeController.GetEmployeeByID(currentUserID);
            Greating = $"Добрый день, {currentUser.EmployeeFirstMiddleName}!";
        }

        public ICommand IOpenPersonellRecordsWindow => new RelayCommand(openDepartment => OpenPersonnelRecordsWindow());

        private void OpenPersonnelRecordsWindow()
        {
            DepartmentsEmployeesPanelWindow departmentsEmployeesPanelWindow = new DepartmentsEmployeesPanelWindow(currentUser.EmployeeID);
            departmentsEmployeesPanelWindow.Show();
            //_adminPanelWindow.Close();
        }

        public ICommand IOpenUserRegistrationWindow => new RelayCommand(openUserRegistrationWindow => OpenUserRegistrationWindow());
        private void OpenUserRegistrationWindow()
        {
            UserRegistrationWindow userRegistrationWindow = new UserRegistrationWindow(currentUser.EmployeeID);
            userRegistrationWindow.Show();
            _adminPanelWindow.Close();
        }

        public ICommand IOpenDocumentRegistrationWindow => new RelayCommand(openDocumentRegistrationWindow => OpenDocumentRegistrationWindow());
        private void OpenDocumentRegistrationWindow()
        {
            DocumentRegistrationWindow documentRegistrationWindow = new DocumentRegistrationWindow(currentUser.EmployeeID);
            documentRegistrationWindow.Show();
        }

        public ICommand IOpenManagerPanelWindow => new RelayCommand(openManagerPanelWindow => OpenManagerPanelWindow());
        private void OpenManagerPanelWindow()
        {
            ManagerPanelWindow managerPanelWindow = new ManagerPanelWindow(currentUser.EmployeeID);
            managerPanelWindow.Show();
        }

        public ICommand IExit => new RelayCommand(exit => { _adminPanelWindow.Close(); });
    }
}
