using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class DepartmentsEmployeesPanelViewModel : BaseViewModelClass
    {
        private DepartmentsEmployeesPanelWindow DepartmentsEmployeesPanelWindow;

        private bool _isEmployee = false;

        private string _labelListContent = "Список отделов";
        public string LabelListContent
        {
            get { return _labelListContent; }
            set
            {
                _labelListContent = value;
                OnPropertyChanged(nameof(LabelListContent));
            }
        }

        private string _addBtnContent = "Добавить отдел";
        public string AddBtnContent
        {
            get { return _addBtnContent; }
            set
            {
                _addBtnContent = value;
                OnPropertyChanged(nameof(AddBtnContent));
            }
        }
        private List<Department> allDepartments { get; set; }
        public ObservableCollection<Department> Departments { get; set; }
        private Department _selectedDepartment = null;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
                if (value != null)
                {
                    GetEmployeesOfDepartment();
                    GetDeptyOfDepartment();
                    GetHeadOfDepartment();
                    GetPerformersOfDepartment();
                }
            }
        }
        private List<Employee> allEmployees { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        private Employee _selectedEmployee = null;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public ObservableCollection<Employee> EmployeesOfDepartment { get; set; }
        private Employee _deputyGeneralDirector;
        public Employee DeputyGeneralDirector
        {
            get => _deputyGeneralDirector;
            set
            {
                _deputyGeneralDirector = value;
                OnPropertyChanged(nameof(DeputyGeneralDirector));
            }
        }
        private Employee _headerOfDepartment;
        public Employee HeaderOfDepartment
        {
            get => _headerOfDepartment;
            set
            {
                _headerOfDepartment = value;
                OnPropertyChanged(nameof(HeaderOfDepartment));
            }
        }
        public ObservableCollection<Employee> PerformersOfDepartment { get; set; }

        private Employee _selectedPerformer = null;
        public Employee SelectedPerformer
        {
            get => _selectedPerformer;
            set
            {
                _selectedPerformer = value;
                OnPropertyChanged(nameof(SelectedPerformer));
                UpdatePerformer();
            }
        }

        private string _searchString;
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }

        public DepartmentsEmployeesPanelViewModel(DepartmentsEmployeesPanelWindow inputWindow)
        {
            DepartmentsEmployeesPanelWindow = inputWindow;
            Departments = new ObservableCollection<Department>();
            EmployeesOfDepartment = new ObservableCollection<Employee>();
            PerformersOfDepartment = new ObservableCollection<Employee>();
            InitializeDepartments();
            InitializeEmployees();
            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitializeDepartments()
        {
            Departments.Clear();
            DepartmentController departmentController = new DepartmentController();
            allDepartments = departmentController.GetAllDepartments();
            if (allDepartments.Count > 0)
            {
                foreach (Department department in allDepartments)
                {
                    Departments.Add(department);
                }
            }
        }

        private void InitializeEmployees()
        {
            Employees = new ObservableCollection<Employee>();
            EmployeeController employeeController = new EmployeeController();
            allEmployees = employeeController.GetAllEmployees();
            if (allEmployees.Count > 0)
            {
                foreach (Employee employee in allEmployees)
                {
                    Department department = Departments.Where(x => x.DepartmentID == employee.DepartmentID).FirstOrDefault();
                    employee.Department = department;
                }
            }
            if (allEmployees.Count > 0)
            {
                foreach (Employee employee in allEmployees)
                {
                    Employees.Add(employee);
                }
            }
            OnPropertyChanged(nameof(Employees));
        }

        private void GetEmployeesOfDepartment()
        {
            EmployeesOfDepartment.Clear();
            if (SelectedDepartment != null) {
                foreach (Employee employee in Employees)
                {
                    if (employee.DepartmentID == SelectedDepartment.DepartmentID)
                    {
                        EmployeesOfDepartment.Add(employee);
                    }
                }
            }
        }

        private void GetDeptyOfDepartment()
        {
            DeputyGeneralDirector = new Employee();
            if(SelectedDepartment != null && EmployeesOfDepartment.Count > 0)
            {
                DeputyGeneralDirector = EmployeesOfDepartment.Where(x => x.EmployeeRole == EmployeeRole.DeputyGeneralDirector).FirstOrDefault();
            }
        }

        private void GetHeadOfDepartment()
        {
            HeaderOfDepartment = new Employee();
            if (SelectedDepartment != null && EmployeesOfDepartment.Count > 0)
            {
                HeaderOfDepartment = EmployeesOfDepartment.Where(x => x.EmployeeRole == EmployeeRole.HeadOfDepartment).FirstOrDefault();
            }
        }

        private void GetPerformersOfDepartment()
        {
            PerformersOfDepartment.Clear();
            if (SelectedDepartment != null && EmployeesOfDepartment.Count > 0)
            {
                foreach (Employee employee in EmployeesOfDepartment)
                {
                    if(employee.EmployeeRole == EmployeeRole.Performer)
                    {
                        PerformersOfDepartment.Add(employee);
                    }
                }
            }
        }

        public ICommand ISelectDepartments => new RelayCommand(selectDepartments => SelectDepartments());
        private void SelectDepartments()
        {
            LabelListContent = "Список отделов";
            AddBtnContent = "Добавить отдел";
            DepartmentsEmployeesPanelWindow.DepartmentList.Visibility = Visibility.Visible;
            DepartmentsEmployeesPanelWindow.EmployeeList.Visibility = Visibility.Hidden;
            DepartmentsEmployeesPanelWindow.DetailDepartmentInfo.Visibility = Visibility.Visible;
            DepartmentsEmployeesPanelWindow.EmployeeDetailInfo.Visibility = Visibility.Hidden;
            _isEmployee = false;
        }

        public ICommand ISelectEmployees => new RelayCommand(selectEmployees => SelectEmployees());
        private void SelectEmployees()
        {
            LabelListContent = "Список сотрудников";
            AddBtnContent = "Добавить сотрудника";
            DepartmentsEmployeesPanelWindow.DepartmentList.Visibility = Visibility.Hidden;
            DepartmentsEmployeesPanelWindow.EmployeeList.Visibility = Visibility.Visible;
            DepartmentsEmployeesPanelWindow.DetailDepartmentInfo.Visibility = Visibility.Hidden;
            DepartmentsEmployeesPanelWindow.EmployeeDetailInfo.Visibility = Visibility.Visible;
            _isEmployee = true;
        }
        public ICommand IFindItem => new RelayCommand(findItem => FindItem());

        private void FindItem()
        {
            FindItems(SearchString);
        }

        public void FindItems(string searchString)
        {
            SearchString = searchString;
            if (!_isEmployee)
            {
                FindDepartment();
            }
            else
            {
                FindEmployee();
            }
        }

        private void FindDepartment()
        {
            Departments.Clear();
            if(string.IsNullOrEmpty(SearchString) || string.IsNullOrWhiteSpace(SearchString)) 
            {
                if (allDepartments.Count > 0)
                {
                    foreach (Department department in allDepartments)
                    {
                        Departments.Add(department);
                    }
                }
                SelectedDepartment = Departments.FirstOrDefault();
                return;
            }
            List<Department> findingDepartment = new List<Department>();
            string tempSearchString = SearchString.ToLower().Trim();
            findingDepartment = allDepartments.Where(x => x.DepartmentTitle.ToLower().Contains(tempSearchString)).ToList();
            if (findingDepartment.Count == 0)
            {
                findingDepartment = allDepartments.Where(x => x.DepartmentShortTitle.ToLower().Contains(tempSearchString)).ToList();
            }
            foreach (Department department in findingDepartment)
            {
                Departments.Add(department);
            }
            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void FindEmployee()
        {
            Employees.Clear();
            if (string.IsNullOrEmpty(SearchString) || string.IsNullOrWhiteSpace(SearchString))
            {
                if (allEmployees.Count > 0)
                {
                    foreach (Employee employee in allEmployees)
                    {
                        Employees.Add(employee);
                    }
                }
                SelectedEmployee = Employees.FirstOrDefault();
                return;
            }
            List<Employee> findingEmployee = new List<Employee>();
            string tempSearchString = SearchString.ToLower().Trim();
            findingEmployee = allEmployees.Where(x => x.EmployeeFullName.ToLower().Contains(tempSearchString)).ToList();
            if (findingEmployee.Count > 0)
            {
                foreach (Employee employee in findingEmployee)
                {
                    Employees.Add(employee);
                }
            }
            SelectedEmployee = Employees.FirstOrDefault();
        }

        public ICommand IAddItem => new RelayCommand(addItem => AddItem());
        private void AddItem()
        {
            if (!_isEmployee)
            {
                AddNewDepartment();
            }
            else
            {
                AddNewEmployee();
            }
        }

        public ICommand IAddNewDepartment => new RelayCommand(addNewDepartment => AddNewDepartment());
        private void AddNewDepartment()
        {
            DepartmentWindow departmentWindow = new DepartmentWindow(null);
            departmentWindow.ShowDialog();
            Departments.Clear();
            InitializeDepartments();
            if (SelectedDepartment != null)
            {
                Department department = Departments.Where(x => x.DepartmentID == SelectedDepartment.DepartmentID).FirstOrDefault();
                if (department != null)
                {
                    SelectedDepartment = department;
                }
            }
            else
            {
                SelectedDepartment = Departments.FirstOrDefault();
            }
        }

        public ICommand IEditSelectedDepartment => new RelayCommand(editSelectedDepartment => EditSelectedDepartment());
        private void EditSelectedDepartment()
        {
            if (SelectedDepartment != null)
            {
                DepartmentWindow departmentWindow = new DepartmentWindow(SelectedDepartment);
                departmentWindow.ShowDialog();
                Departments.Clear();
                InitializeDepartments();
                if (SelectedDepartment != null)
                {
                    Department department = Departments.Where(x => x.DepartmentID == SelectedDepartment.DepartmentID).FirstOrDefault();
                    if (department != null)
                    {
                        SelectedDepartment = department;
                    }
                }
                else
                {
                    SelectedDepartment = Departments.FirstOrDefault();
                }
            }
        }

        public ICommand IAddNewEmployee => new RelayCommand(addEmployee => AddNewEmployee());
        private void AddNewEmployee()
        {
            EmployeeWindow employeeWindow = new EmployeeWindow(null);
            employeeWindow.ShowDialog();
            Employees.Clear();
            InitializeEmployees();
            if (SelectedEmployee != null)
            {
                Employee employee = Employees.Where(x => x.EmployeeID == SelectedEmployee.EmployeeID).FirstOrDefault();
                if (employee != null)
                {
                    SelectedEmployee = employee;
                }
            }
            else
            {
                SelectedEmployee = Employees.FirstOrDefault();
            }
        }
        public ICommand IUpdateEmployee => new RelayCommand(updateEmployee => UpdateEmployee());
        private void UpdateEmployee()
        {
            if (SelectedEmployee != null)
            {
                UpdateCurrentEmployee(SelectedEmployee);
            }
        }

        private void UpdatePerformer()
        {
            if (SelectedPerformer != null)
            {
                UpdateCurrentEmployee(SelectedPerformer);
            }
        }
        private void UpdateCurrentEmployee(Employee inputEmployee)
        {
            EmployeeWindow employeeWindow = new EmployeeWindow(inputEmployee);
            employeeWindow.Show();
            Employees.Clear();
            InitializeEmployees();
            if (SelectedEmployee != null)
            {
                Employee employee = Employees.Where(x => x.EmployeeID == SelectedEmployee.EmployeeID).FirstOrDefault();
                if (employee != null)
                {
                    SelectedEmployee = employee;
                }
            }
            else
            {
                SelectedEmployee = Employees.FirstOrDefault();
            }
        }

    }
}
