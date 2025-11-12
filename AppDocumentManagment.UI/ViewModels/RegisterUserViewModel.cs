using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagment.UI.ViewModels
{
    public class RegisterUserViewModel : BaseViewModelClass
    {
        private RegisterUserWindow RegisterUserWindow;
        private Employee _employee;

        private string _userImagePath;
        public string UserImagePath
        {
            get => _userImagePath;
            set
            {
                _userImagePath = value;
                OnPropertyChanged(nameof(UserImagePath));
            }
        }

        private string _userFirstName;
        public string UserFirstName
        {
            get => _userFirstName;
            set
            {
                _userFirstName = value;
                OnPropertyChanged(nameof(UserFirstName));
            }
        }
        private string _userLastName;
        public string UserLastName
        {
            get => _userLastName;
            set
            {
                _userLastName = value;
                OnPropertyChanged(nameof(UserLastName));
            }
        }

        private string _userMiddleName;
        public string UserMiddleName
        {
            get => _userMiddleName;
            set
            {
                _userMiddleName = value;
                OnPropertyChanged(nameof(UserMiddleName));
            }
        }

        private string _userDepartmentTitle;
        public string UserDepartmentTitle
        {
            get => _userDepartmentTitle;
            set
            {
                _userDepartmentTitle = value;
                OnPropertyChanged(nameof(UserDepartmentTitle));
            }
        }

        private string _userPosition;
        public string UserPosition
        {
            get => _userPosition;
            set
            {
                _userPosition = value;
                OnPropertyChanged(nameof(UserPosition));
            }
        }

        public List<string> UserRoles { get; set; }

        private UserRole _selectedUserRole;

        public UserRole SelectedUserRole
        {
            get => _selectedUserRole;
            set
            {
                _selectedUserRole = value;
                OnPropertyChanged(nameof(UserRole));
                SelectedUserRoleIndex = UserRoleConverter.ToIntConvert(value);
                OnPropertyChanged(nameof(SelectedUserRoleIndex));
            }
        }

        private int _userRoleIndex;
        public int SelectedUserRoleIndex
        {
            get => _userRoleIndex;
            set
            {
                if (_userRoleIndex != value)
                {
                    _userRoleIndex = value;
                    OnPropertyChanged(nameof(SelectedUserRoleIndex));
                    SelectedUserRole = UserRoleConverter.BackConvert(value);
                    OnPropertyChanged(nameof(SelectedUserRoleIndex));
                }
            }
        }

        private string _userLogin;
        public string UserLogin
        {
            get => _userLogin;
            set
            {
                _userLogin = value;
                OnPropertyChanged(nameof(UserLogin));
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get => _userPassword;
            set
            {
                _userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }

        private bool _isRegistred = false;
        private int _userId = 0;

        public RegisterUserViewModel(RegisterUserWindow window, Employee selectedEmployee, bool isRegistred)
        {
            RegisterUserWindow = window;
            if (!isRegistred)
            {
                RegisterUserWindow.DeleteBtn.Visibility = Visibility.Hidden;
                RegisterUserWindow.EditBtn.Content = "Зарегистрировать";
            }
            else
            {
                RegisterUserWindow.DeleteBtn.Visibility = Visibility.Visible;
                RegisterUserWindow.EditBtn.Content = "Сохранить изменения";
            }
            _employee = selectedEmployee;
            InitializeUserPhoto();
            UserFirstName = selectedEmployee.EmployeeFirstName;
            UserLastName = selectedEmployee.EmployeeLastName;
            UserMiddleName = selectedEmployee.EmployeeMiddleName;
            UserDepartmentTitle = selectedEmployee.Department.DepartmentTitle;
            UserPosition = selectedEmployee.Position;
            InitializeUserRoles();
            GetUserLogin();
        }

        private void InitializeUserRoles()
        {
            UserRoles = new List<string>();
            var userRoles = Enum.GetValues(typeof(UserRole));
            foreach (var role in userRoles)
            {
                UserRoles.Add(UserRoleConverter.ConvertToString(role));
            }
            SelectedUserRoleIndex = 4;
        }

        private void InitializeUserPhoto()
        {
            EmployeePhoto employeePhoto = new EmployeePhoto();
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            employeePhoto = employeePhotoController.GetEmployeePhotoByID(_employee.EmployeeID);
            if (employeePhoto != null)
            {
                UserImagePath = FileProcessing.SaveEmployeePhotoToTempFolder(employeePhoto);
            }
        }

        private void GetUserLogin()
        {
            RegistredUserController registredUserController = new RegistredUserController();
            RegistredUser registredUser = registredUserController.GetRegistredUser(_employee.EmployeeID);
            if (registredUser != null)
            {
                _userId = registredUser.RegistredUserID;
                UserLogin = registredUser.RegistredUserLogin;
                _isRegistred = registredUser.IsRegistered;
            }
        }

        public ICommand IRegisterUser => new RelayCommand(registerUser => RegisterUser());
        private void RegisterUser()
        {
            if (!ValidateUser()) return;
            if (string.IsNullOrEmpty(UserPassword))
            {
                MessageBox.Show("Не введен пароль пользователя");
                return;
            }
            if (string.IsNullOrEmpty(UserLogin))
            {
                MessageBox.Show("Не введен логин пользователя");
                return;
            }
            RegistredUser registredUser = new RegistredUser();
            registredUser.RegistredUserLogin = UserLogin;
            string passwordforhasher = $"{UserLogin}-{UserPassword}";
            registredUser.RegistredUserPassword = PassHasher.CalculateMD5Hash(passwordforhasher);
            registredUser.UserRole = SelectedUserRole;
            registredUser.RegistredUserTime = DateTime.Now;
            registredUser.EmployeeID = _employee.EmployeeID;
            bool result = false;
            if (!_isRegistred)
            {
                registredUser.IsRegistered = true;
                RegistredUserController registredUserController = new RegistredUserController();
                result = registredUserController.AddRegistratedUser(registredUser);
            }
            else
            {
                registredUser.RegistredUserID = _userId;
                RegistredUserController registredUserController = new RegistredUserController();
                result = registredUserController.UpdateRegistratedUser(registredUser);
            }
            if (result)
            {
                MessageBox.Show($"Пользователь {registredUser.RegistredUserLogin} зарегистрирован");
                RegisterUserWindow.Close();
            }
            else
            {
                MessageBox.Show($"Ошибка! Пользователь {registredUser.RegistredUserLogin} не зарегистрирован");
            }
        }

        private bool ValidateUser()
        {
            if (string.IsNullOrEmpty(UserLogin))
            {
                MessageBox.Show("Введите логин для сотрудника");
                RegisterUserWindow.EmployeeLogin.BorderThickness = new System.Windows.Thickness(2);
                RegisterUserWindow.EmployeeLogin.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                RegisterUserWindow.EmployeeLogin.BorderThickness = new System.Windows.Thickness(2);
                RegisterUserWindow.EmployeeLogin.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(UserPassword))
            {
                MessageBox.Show("Введите пароль для сотрудника");
                RegisterUserWindow.EmployeePassword.BorderThickness = new System.Windows.Thickness(2);
                RegisterUserWindow.EmployeePassword.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                RegisterUserWindow.EmployeePassword.BorderThickness = new System.Windows.Thickness(2);
                RegisterUserWindow.EmployeePassword.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            return true;
        }

        public ICommand IRemoveRegistration => new RelayCommand(removeRegistration => RemoveRegistration());
        private void RemoveRegistration()
        {
            if (_employee != null)
            {
                bool result = false;
                RegistredUserController registredUserController = new RegistredUserController();
                RegistredUser registredUser = registredUserController.GetRegistredUser(_employee.EmployeeID);
                if (registredUser != null)
                {
                    registredUserController.RemoveRegistratedUser(registredUser);
                    result = true;
                }
                if (result)
                {
                    MessageBox.Show($"Регистрация пользователя {_employee.EmployeeFullName} была отменена");
                }
                else
                {
                    MessageBox.Show($"Ошибка! Регистрация пользователя {_employee.EmployeeFullName} не отменена");
                }
                RegisterUserWindow.Close();
                return;
            }
            MessageBox.Show($"Неизвестная ошибка!");
        }

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            RegisterUserWindow.Close();
        }
    }
}
