using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ExaminingPersonViewModel : BaseViewModelClass
    {
        ExaminingPersonsWindow ExaminingPersonsWindow;

        private List<Employee> ManagersList {  get; set; }

        private List<Department> DepartmentsList { get; set; }

        public ObservableCollection<Employee> Managers { get; set; }

        private Employee selectedManager;
        public Employee SelectedManager
        {
            get => selectedManager;
            set
            {
                selectedManager = value;
                OnPropertyChanged(nameof(SelectedManager));
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
                GetManagerBySearchString(value);
            }
        }

        public ExaminingPersonViewModel(ExaminingPersonsWindow examiningPersonsWindow)
        {
            ExaminingPersonsWindow = examiningPersonsWindow;
            ManagersList = new List<Employee>();
            DepartmentsList = new List<Department>();
            Managers = new ObservableCollection<Employee>();
            GetManagersList();
            GetDepartments();
            InitializeManagers();
        }

        private void GetManagersList()
        {
            ManagersList.Clear();
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = new List<Employee>();
            employees = employeeController.GetAllEmployees();
            foreach (Employee employee in employees)
            {
                if(employee.EmployeeRole != EmployeeRole.Performer)
                {
                    ManagersList.Add(employee);
                }
            }
        }

        private void GetDepartments()
        {
            DepartmentsList.Clear();
            DepartmentController departmentController = new DepartmentController();
            DepartmentsList = departmentController.GetAllDepartments();
        }

        private void InitializeManagers()
        {
            Managers.Clear();
            if (ManagersList.Count > 0)
            {
                ManagersList.Sort((m1, m2) => m1.DepartmentID.CompareTo(m2.DepartmentID));
                foreach (Employee manager in ManagersList)
                {
                    if (DepartmentsList.Count > 0)
                    {
                        Department department = DepartmentsList.Where(d => d.DepartmentID == manager.DepartmentID).FirstOrDefault();
                        manager.Department = department;
                    }
                    Managers.Add(manager);
                }
            }
        }

        public void GetManagerBySearchString(string searchingString)
        {
            string searchString = searchingString.Trim();
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                InitializeManagers();
                return;
            }
            Managers.Clear();
            if (ManagersList?.Count > 0)
            {
                foreach (var manager in ManagersList)
                {
                    if (manager.EmployeeFullName.ToLower().Contains(searchString.ToLower()))
                    {
                        Managers.Add(manager);
                    }
                }
                if(Managers.Count == 0)
                {
                    foreach (var manager in ManagersList)
                    {
                        if (manager.Department.DepartmentTitle.ToLower().Contains(searchString.ToLower()))
                        {
                            Managers.Add(manager);
                        }
                    }
                }
            }
        }

        public ICommand ISendDocument => new RelayCommand(sendDocument => SendDocument());
        private void SendDocument()
        {
            if(SelectedManager != null)
            {
                ExaminingPersonsWindow.Close();
            }
        }
    }
}
