using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class UserRegistrationViewModel : BaseViewModelClass
    {
        UserRegistrationWindow UserRegistrationWindow;

        public ObservableCollection<Department> Departments { get; private set; }
        public ObservableCollection<string> ComboDepartments { get; private set; }
        public ObservableCollection<Employee> Employees { get; private set; }
        public ObservableCollection<RegistredUser> RegistratedUsers { get; private set; }
        public ObservableCollection<User> Users { get; private set; }
        public ObservableCollection<User> AllUsers { get; private set; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
                if (SelectedEmployee != null)
                {
                    OpenResterUserWindow();
                }
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                if (_selectedUser != null)
                {
                    SelectedEmployee = Employees.Where(x => x.EmployeeID == _selectedUser.EmployeeID).FirstOrDefault();
                }
            }
        }

        private int _comboSelectedIndex;
        public int ComboSelectedIndex
        {
            get => _comboSelectedIndex;
            set
            {
                _comboSelectedIndex = value;
                OnPropertyChanged(nameof(ComboSelectedIndex));
            }
        }

        private string _comboSelectedDepartment;
        public string ComboSelectedDepartment
        {
            get => _comboSelectedDepartment;
            set
            {
                _comboSelectedDepartment = value;
                OnPropertyChanged(nameof(ComboSelectedDepartment));
                int index = 0;
                for (; index < ComboDepartments.Count; index++)
                {
                    if (value.Equals(ComboDepartments[index]))
                    {
                        ComboSelectedIndex = index;
                        break;
                    }
                }
                if (value != null)
                {
                    GetUsersByStatus();
                    GetUsersBySelectedDepartmentTitle();
                    if (!string.IsNullOrEmpty(SearchString))
                    {
                        GetUserBySearchString();
                    }
                }
            }
        }

        private UserStatus usersStatus { get; set; } = UserStatus.Registrated;

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                GetUserBySearchString();
            }
        }

        public UserRegistrationViewModel(UserRegistrationWindow userRegistrationWindow)
        {
            UserRegistrationWindow = userRegistrationWindow;
            Departments = new ObservableCollection<Department>();
            ComboDepartments = new ObservableCollection<string>();
            Employees = new ObservableCollection<Employee>();
            RegistratedUsers = new ObservableCollection<RegistredUser>();
            AllUsers = new ObservableCollection<User>();
            Users = new ObservableCollection<User>();
            InitializeDepartments();
            InitializeEmployees();
            InitializeRegistratedUsers();
            InitialiseAllUsers();
            InitializeComboDepartments();
            GetRegistratedUsersList();
        }

        private void InitializeDepartments()
        {
            DepartmentController departmentController = new DepartmentController();
            List<Department> departments = departmentController.GetAllDepartments();
            if (departments.Count > 0)
            {
                foreach (Department department in departments)
                {
                    Departments.Add(department);
                }
            }
        }

        private void InitializeComboDepartments()
        {
            ComboDepartments.Clear();
            ComboDepartments.Add("Все отделы/департаменты");
            foreach (var user in AllUsers)
            {
                if (ComboDepartments.Contains(user.EmployeeDepartmentTitle)) continue;
                ComboDepartments.Add(user.EmployeeDepartmentTitle);
            }
            ComboSelectedDepartment = ComboDepartments.FirstOrDefault();
        }


        private void InitializeEmployees()
        {
            Employees.Clear();
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = employeeController.GetAllEmployees();
            if (employees.Count > 0)
            {
                foreach (Employee employee in employees)
                {
                    Department deparment = Departments.Where(x => x.DepartmentID == employee.DepartmentID).FirstOrDefault();
                    employee.Department = deparment;
                    employee.DepartmentID = deparment.DepartmentID;
                    Employees.Add(employee);
                }
            }
        }

        private void InitializeRegistratedUsers()
        {
            RegistratedUsers.Clear();
            RegistredUserController registeredUsersController = new RegistredUserController();
            List<RegistredUser> registredUsers = registeredUsersController.GetAllRegistredUsers();
            if (registredUsers.Count > 0)
            {
                foreach (RegistredUser registeredUser in registredUsers)
                {
                    RegistratedUsers.Add(registeredUser);
                }
            }
        }

        private void InitialiseAllUsers()
        {
            AllUsers.Clear();
            foreach (Employee employee in Employees)
            {
                User registredUser = new User();
                registredUser.EmployeeID = employee.EmployeeID;
                registredUser.EmployeeImagePath = employee.EmployeeImagePath;
                registredUser.EmployeeFullName = employee.EmployeeFullName;
                registredUser.EmployeePosition = employee.Position;
                registredUser.EmployeeDepartmentTitle = employee.Department.DepartmentTitle;
                RegistredUser user = RegistratedUsers.Where(x => x.EmployeeID == employee.EmployeeID).FirstOrDefault();
                if (user != null)
                {
                    registredUser.Userlogin = user.RegistredUserLogin;
                    registredUser.UserRole = user.UserRole.ToString();
                    registredUser.UserIsRegistrated = user.IsRegistered;
                }
                AllUsers.Add(registredUser);
            }
        }

        public ICommand IGetAllUsersList => new RelayCommand(getAllUsersList => GetAllUsersList());
        private void GetAllUsersList()
        {
            usersStatus = UserStatus.All;
            GetUsersByStatus();
            GetUsersBySelectedDepartmentTitle();
            if (!string.IsNullOrEmpty(SearchString))
            {
                GetUserBySearchString();
            }
        }

        private void GetAllUsers()
        {
            Users.Clear();
            foreach (var user in AllUsers)
            {
                Users.Add(user);
            }
        }

        public ICommand IGetRegistratedUsersList => new RelayCommand(getRegistratedUsersList => GetRegistratedUsersList());
        private void GetRegistratedUsersList()
        {
            usersStatus = UserStatus.Registrated;
            GetUsersByStatus();
            GetUsersBySelectedDepartmentTitle();
            if (!string.IsNullOrEmpty(SearchString))
            {
                GetUserBySearchString();
            }
        }

        private void GetRegistratedUsers()
        {
            Users.Clear();
            foreach (var user in AllUsers)
            {
                if (user.UserIsRegistrated)
                {
                    Users.Add(user);
                }
            }
        }
        public ICommand IGetNotRegistratedUsersList => new RelayCommand(getNotRegistatedUsersList => GetNotRegistratedUsersList());
        private void GetNotRegistratedUsersList()
        {
            usersStatus = UserStatus.NotRegistrated;
            GetUsersByStatus();
            GetUsersBySelectedDepartmentTitle();
            if (!string.IsNullOrEmpty(SearchString))
            {
                GetUserBySearchString();
            }
        }
        private void GetNotRegistratedUsers()
        {
            Users.Clear();
            foreach (var user in AllUsers)
            {
                if (!user.UserIsRegistrated)
                {
                    Users.Add(user);
                }
            }
        }

        private void GetUsersByStatus()
        {
            switch (usersStatus)
            {
                case UserStatus.All:
                    GetAllUsers();
                    break;
                case UserStatus.Registrated:
                    GetRegistratedUsers();
                    break;
                case UserStatus.NotRegistrated:
                    GetNotRegistratedUsers();
                    break;
            }
        }

        private void GetUsersBySelectedDepartmentTitle()
        {
            List<User> users = new List<User>();
            foreach (var user in Users)
            {
                users.Add(user);
            }
            Users.Clear();
            foreach (var user in users)
            {
                if (ComboSelectedDepartment.Equals("Все отделы/департаменты"))
                {
                    Users.Add(user);
                }
                else if (user.EmployeeDepartmentTitle.Equals(ComboSelectedDepartment))
                {
                    Users.Add(user);
                }
            }
        }

        public void FindUserBySearchString(string searchString)
        {
            GetUsersByStatus();
            GetUsersBySelectedDepartmentTitle();
            SearchString = searchString;
        }

        private void GetUserBySearchString()
        {
            string searchString = SearchString.Trim();
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                return;
            }
            else
            {
                List<User> users = new List<User>();
                foreach (var user in Users)
                {
                    if (user.EmployeeFullName.ToLower().Contains(searchString.ToLower()))
                    {
                        users.Add(user);
                    }
                }
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
        }

        private void OpenResterUserWindow()
        {
            RegisterUserWindow registerUserWindow = new RegisterUserWindow(SelectedEmployee, SelectedUser.UserIsRegistrated);
            registerUserWindow.ShowDialog();
            InitializeRegistratedUsers();
            InitialiseAllUsers();
            InitializeComboDepartments();
            GetAllUsers();
            GetRegistratedUsers();
            GetNotRegistratedUsers();
        }

        public ICommand IBack => new RelayCommand(back => Back());
        private void Back()
        {
            AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
            adminPanelWindow.Show();
            UserRegistrationWindow.Close();
        }

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            UserRegistrationWindow.Close();
        }
    }

    public class User()
    {
        public int EmployeeID { get; set; }
        public string EmployeeImagePath { get; set; } = "Отсутствует";
        public string EmployeeFullName { get; set; } = "Отсутствует";
        public string EmployeePosition { get; set; } = "Отсутствует";
        public string EmployeeDepartmentTitle { get; set; } = "Отсутствует";
        public string Userlogin { get; set; } = "Отсутствует";
        public string UserRole { get; set; } = "Отсутствует";
        public bool UserIsRegistrated { get; set; } = false;
    }

    public enum UserStatus
    {
        All = 0,
        Registrated = 1,
        NotRegistrated = 2,
    }
}

