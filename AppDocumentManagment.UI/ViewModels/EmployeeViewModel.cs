using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagment.UI.ViewModels
{
    public class EmployeeViewModel : BaseViewModelClass
    {
        private IFileDialogService _imageDialogService;

        private EmployeeWindow EmployeeWindow;

        private Employee employee;

        private string _employeeLastName;
        public string EmployeeLastName
        {
            get => _employeeLastName;
            set
            {
                _employeeLastName = value;
                OnPropertyChanged(nameof(EmployeeLastName));
            }
        }

        private string _employeeFirstName;
        public string EmployeeFirstName
        {
            get => _employeeFirstName;
            set
            {
                _employeeFirstName = value;
                OnPropertyChanged(nameof(EmployeeFirstName));
            }
        }

        private string _employeeMiddleName;
        public string EmployeeMiddleName
        {
            get => _employeeMiddleName;
            set
            {
                _employeeMiddleName = value;
                OnPropertyChanged(nameof(EmployeeMiddleName));
            }
        }

        private string _employeePosition;
        public string EmployeePosition
        {
            get => _employeePosition;
            set
            {
                _employeePosition = value;
                OnPropertyChanged(nameof(EmployeePosition));
            }
        }

        public List<string> EmployeeRoles { get; set; }

        private EmployeeRole _selectedEmploeeRole = EmployeeRole.Performer;
        public EmployeeRole SelectedEmployeeRole
        {
            get => _selectedEmploeeRole;
            set
            {
                _selectedEmploeeRole = value;
                OnPropertyChanged(nameof(SelectedEmployeeRole));
                SelectedEmployeeIndex = EmployeeRoleConverter.ToIntConvert(value);
                OnPropertyChanged(nameof(SelectedEmployeeIndex));
            }
        }

        private int _selectedEmployeeIndex = 3;
        public int SelectedEmployeeIndex
        {
            get
            {
                return _selectedEmployeeIndex;
            }
            set
            {
                if (_selectedEmployeeIndex != value)
                {
                    _selectedEmployeeIndex = value;
                    OnPropertyChanged(nameof(SelectedEmployeeIndex));
                    SelectedEmployeeRole = EmployeeRoleConverter.BackConvert(value);
                    OnPropertyChanged(nameof(SelectedEmployeeRole));
                }
            }
        }

        public List<string> DepartmentTitleList { get; set; }
        public List<Department> DepartmentList { get; set; }
        private Department _selectedDepartment = null;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
                SelectedDepartmentIndex = DepartmentConverter.DepartmentToInt(value, DepartmentList);
            }
        }

        private int _selectedDepartmentIndex = 0;
        public int SelectedDepartmentIndex
        {
            get => _selectedDepartmentIndex;
            set
            {
                if (_selectedDepartmentIndex != value)
                {
                    _selectedDepartmentIndex = value;
                    OnPropertyChanged(nameof(SelectedDepartmentIndex));
                    SelectedDepartment = DepartmentConverter.IntToDepartment(value, DepartmentList);
                }
            }
        }

        private string _employeeImagePath = "/Resources/Images/defaultContact.png";
        public string EmployeeImagePath
        {
            get => _employeeImagePath;
            set
            {
                _employeeImagePath = value;
                OnPropertyChanged(nameof(EmployeeImagePath));
            }
        }

        private int SelectedEmployeeID = 0;

        private string _employeePhone;
        public string EmployeePhone
        {
            get => _employeePhone;
            set
            {
                _employeePhone = value;
                OnPropertyChanged(nameof(EmployeePhone));
            }
        }

        private string _employeeEmail;
        public string EmployeeEmail
        {
            get => _employeeEmail;
            set
            {
                _employeeEmail = value;
                OnPropertyChanged(nameof(EmployeeEmail));
            }
        }

        private string _employeeInformation;
        public string EmployeeInformation
        {
            get => _employeeInformation;
            set
            {
                _employeeInformation = value;
                OnPropertyChanged(nameof(EmployeeInformation));
            }
        }

        private string removeBtnTitle = "Очистить данные";
        public string RemoveBtnTitle
        {
            get => removeBtnTitle;
            set
            {
                removeBtnTitle = value;
                OnPropertyChanged(nameof(RemoveBtnTitle));
            }
        }
        public EmployeeViewModel(EmployeeWindow employeeWindow)
        {
            EmployeeWindow = employeeWindow;
            InitializeEmployeeRoles();
            InitializeDepartments();
            SelectedDepartment = DepartmentList.FirstOrDefault();
            _imageDialogService = new WindowsDialogService();
        }

        public EmployeeViewModel(EmployeeWindow employeeWindow, Employee selectedEmployee)
        {
            employee = selectedEmployee;
            EmployeeWindow = employeeWindow;
            InitializeEmployeeRoles();
            InitializeDepartments();
            _imageDialogService = new WindowsDialogService();
            EmployeeFirstName = selectedEmployee.EmployeeFirstName;
            EmployeeLastName = selectedEmployee.EmployeeLastName;
            EmployeeMiddleName = selectedEmployee.EmployeeMiddleName;
            SelectedEmployeeRole = selectedEmployee.EmployeeRole;
            EmployeePosition = selectedEmployee.Position;
            SelectedDepartment = selectedEmployee.Department;
            EmployeeImagePath = selectedEmployee.EmployeeImagePath;
            SelectedEmployeeID = selectedEmployee.EmployeeID;
            EmployeePhone = selectedEmployee.EmployeePhone;
            EmployeeEmail = selectedEmployee.EmployeeEmail;
            EmployeeInformation = selectedEmployee.EmployeeInformation;
            RemoveBtnTitle = "Удалить сотрудника";
        }

        private void InitializeEmployeeRoles()
        {
            EmployeeRoles = new List<string>();
            var employeeRoles = Enum.GetValues(typeof(EmployeeRole));
            foreach (var role in employeeRoles)
            {
                EmployeeRoles.Add(EmployeeRoleConverter.ConvertToString(role));
            }
        }

        private void InitializeDepartments()
        {
            DepartmentTitleList = new List<string>();
            DepartmentController controller = new DepartmentController();
            DepartmentList = controller.GetAllDepartments();
            foreach (var department in DepartmentList)
            {
                DepartmentTitleList.Add(DepartmentConverter.DepartmentToString(department));
            }
        }

        public ICommand IRemove => new RelayCommand(remove => Remove());
        private void Remove()
        {
            if (employee != null)
            {
                EmployeeController employeeController = new EmployeeController();
                bool result = employeeController.DeleteEmployee(employee);
                if (result)
                {
                    MessageBox.Show($"Удаление {employee.EmployeeFullName} выполнено");
                    EmployeeWindow.Close();
                }
                else
                {
                    MessageBox.Show($"Ошибка. Удаление {employee.EmployeeFullName} не выполнено");
                    EmployeeWindow.Close();
                }
            }
            else
            {
                ClearFields();
            }
        }

        public ICommand ISave => new RelayCommand(save => Save());
        private void Save()
        {
            if (!ValidateEmployee()) return;
            Employee newEmployee = new Employee();
            newEmployee.EmployeeFirstName = EmployeeFirstName;
            newEmployee.EmployeeLastName = EmployeeLastName;
            newEmployee.EmployeeMiddleName = EmployeeMiddleName;
            newEmployee.EmployeeRole = SelectedEmployeeRole;
            newEmployee.Position = EmployeePosition;
            newEmployee.DepartmentID = SelectedDepartment.DepartmentID;
            //newEmployee.Department = SelectedDepartment;
            newEmployee.EmployeeImagePath = EmployeeImagePath;
            newEmployee.EmployeePhone = EmployeePhone;
            newEmployee.EmployeeEmail = EmployeeEmail;
            newEmployee.EmployeeInformation = EmployeeInformation;
            if (SelectedEmployeeID == 0)
            {
                EmployeeController controller = new EmployeeController();
                controller.AddEmployee(newEmployee);
            }
            else
            {
                newEmployee.EmployeeID = SelectedEmployeeID;
                EmployeeController controller = new EmployeeController();
                controller.UpdateEmployee(newEmployee);
            }
            MessageBox.Show("Сохранение выполнено");
            EmployeeWindow.Close();
        }

        private bool ValidateEmployee()
        {
            if (string.IsNullOrEmpty(EmployeeLastName))
            {
                MessageBox.Show("Введите фамилию сотрудника");
                EmployeeWindow.EmployeeLastName.BorderThickness = new System.Windows.Thickness(2);
                EmployeeWindow.EmployeeLastName.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                EmployeeWindow.EmployeeLastName.BorderThickness = new System.Windows.Thickness(2);
                EmployeeWindow.EmployeeLastName.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(EmployeeFirstName))
            {
                MessageBox.Show("Введите имя сотрудника");
                EmployeeWindow.EmployeeFirstName.BorderThickness = new System.Windows.Thickness(2);
                EmployeeWindow.EmployeeFirstName.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                EmployeeWindow.EmployeeFirstName.BorderThickness = new System.Windows.Thickness(2);
                EmployeeWindow.EmployeeFirstName.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(EmployeePosition))
            {
                MessageBox.Show("Введите должность сотрудника");
                EmployeeWindow.EmployeePosition.BorderThickness = new System.Windows.Thickness(2);
                EmployeeWindow.EmployeePosition.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                EmployeeWindow.EmployeePosition.BorderThickness = new System.Windows.Thickness(2);
                EmployeeWindow.EmployeePosition.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            return true;
        }

        public ICommand IBrowseEmployeeImage => new RelayCommand(browseEmployeeImage => EmployeeBrowseImage());
        private void EmployeeBrowseImage()
        {
            var filePath = _imageDialogService.OpenFile("Image files|*.bmp;*.jpg;*.jpeg;*.png|All files");
            EmployeeImagePath = filePath;
        }

        private void ClearFields()
        {
            EmployeeFirstName = "";
            EmployeeLastName = "";
            EmployeeMiddleName = "";
            EmployeePosition = "";
            SelectedEmployeeRole = EmployeeRole.Performer;
            SelectedDepartment = DepartmentList.FirstOrDefault();
            EmployeeImagePath = "/Resources/Images/defaultContact.png";
            SelectedEmployeeID = 0;
            SelectedDepartmentIndex = 0;
            EmployeePhone = "";
            EmployeeEmail = "";
        }

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            ClearFields();
            EmployeeWindow.Close();
        }
    }
}
