using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ObservableCollection<Department> Departments { get; private set; }
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

        public ObservableCollection<Employee> Employees { get; private set; }

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

        public ObservableCollection<Employee> EmployeesOfDepartment { get; private set; }
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
        public ObservableCollection<Employee> PerformersOfDepartment { get; private set; }

        public DepartmentsEmployeesPanelViewModel(DepartmentsEmployeesPanelWindow inputWindow)
        {
            DepartmentsEmployeesPanelWindow = inputWindow;
            InitializeDepartments();
            InitializeEmployees();
            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitializeDepartments()
        {
            Departments = new ObservableCollection<Department>();
            DepartmentController departmentController = new DepartmentController();
            List<Department> departmentList = departmentController.GetAllDepartments();
            if (departmentList.Count > 0)
            {
                foreach (Department department in departmentList)
                {
                    Departments.Add(department);
                }
            }
        }

        private void InitializeEmployees()
        {
            Employees = new ObservableCollection<Employee>();
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employeeList = employeeController.GetAllEmployees();
            if (employeeList.Count > 0)
            {
                foreach (Employee employee in employeeList)
                {
                    Employees.Add(employee);
                }
            }
        }

        private void GetEmployeesOfDepartment()
        {
            EmployeesOfDepartment = new ObservableCollection<Employee>();
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
            PerformersOfDepartment = new ObservableCollection<Employee>();
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

    }
}
