using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ExaminingPersonViewModel : BaseViewModelClass
    {
        ExaminingPersonsWindow ExaminingPersonsWindow;

        private bool NeedManager = false;
        private List<Department> DepartmentsList { get; set; }

        private List<EmployeePhoto> EmployeePhotos { get; set; }
        private List<Employee> EmployeesList {  get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
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
                GetEmployeeBySearchString(value);
            }
        }

        private string panelName;
        public string PanelName
        {
            get => panelName;
            set
            {
                panelName = value;
                OnPropertyChanged(nameof(PanelName));
            }
        }

        private string selectButtonName;
        public string SelectButtonName
        {
            get => selectButtonName;
            set
            {
                selectButtonName = value;
                OnPropertyChanged(nameof(SelectButtonName));
            }
        }

        public ExaminingPersonViewModel(ExaminingPersonsWindow examiningPersonsWindow, bool needManager)
        {
            ExaminingPersonsWindow = examiningPersonsWindow;
            NeedManager = needManager;
            PanelName = NeedManager == true ? "Список руководства организации" : "Список сотрудников";
            SelectButtonName = NeedManager == true ? "Выбрать руководителя" : "Выбрать сотрудника";
            EmployeesList = new List<Employee>();
            DepartmentsList = new List<Department>();
            Employees = new ObservableCollection<Employee>();
            GetDepartments();
            GetEmployeesPhotos();
            GetEmployeeList();
            InitializeEmployees();
        }

        private void GetEmployeeList()
        {
            EmployeesList.Clear();
            EmployeeController employeeController = new EmployeeController();
            List<Employee> employees = new List<Employee>();
            employees = employeeController.GetAllEmployees();
            if (NeedManager)
            {
                foreach (Employee employee in employees)
                {
                    if (employee.EmployeeRole != EmployeeRole.Performer)
                    {
                        EmployeesList.Add(employee);
                    }
                }
            }
            else
            {
                foreach (Employee employee in employees)
                {
                    EmployeesList.Add(employee);
                }
            }
            foreach (Employee employee in EmployeesList)
            {
                employee.EmployeePhoto = EmployeePhotos.Where(p => p.EmployeeID == employee.EmployeeID).FirstOrDefault();
            }
        }

        private void GetDepartments()
        {
            DepartmentsList.Clear();
            DepartmentController departmentController = new DepartmentController();
            DepartmentsList = departmentController.GetAllDepartments();
        }

        private void GetEmployeesPhotos()
        {
            EmployeePhotos = new List<EmployeePhoto>();
            EmployeePhotoController employeePhotoController = new EmployeePhotoController();
            EmployeePhotos = employeePhotoController.GetEmployeePhotos();
            foreach (EmployeePhoto photo in EmployeePhotos)
            {
                string photoPath = FileProcessing.SaveEmployeePhotoToTempFolder(photo);
                photo.FilePath = photoPath;
            }
        }

        private void InitializeEmployees()
        {
            Employees.Clear();
            if (EmployeesList.Count > 0)
            {
                EmployeesList.Sort((m1, m2) => m1.DepartmentID.CompareTo(m2.DepartmentID));
                foreach (Employee employee in EmployeesList)
                {
                    if (DepartmentsList.Count > 0)
                    {
                        Department department = DepartmentsList.Where(d => d.DepartmentID == employee.DepartmentID).FirstOrDefault();
                        if (department != null)
                        {
                            employee.Department = department;
                        }
                        EmployeePhoto employeePhoto = EmployeePhotos.Where(p => p.EmployeeID == employee.EmployeeID).FirstOrDefault();
                        if (employeePhoto != null)
                        {
                            employee.EmployeePhoto = employeePhoto;
                        }
                    }
                    Employees.Add(employee);
                }
            }
        }

        public void GetEmployeeBySearchString(string searchingString)
        {
            string searchString = searchingString.Trim();
            if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrEmpty(searchString))
            {
                InitializeEmployees();
                return;
            }
            Employees.Clear();
            if (EmployeesList?.Count > 0)
            {
                foreach (var employee in EmployeesList)
                {
                    if (employee.EmployeeFullName.ToLower().Contains(searchString.ToLower()))
                    {
                        Employees.Add(employee);
                    }
                }
                if(Employees.Count == 0)
                {
                    foreach (var employee in EmployeesList)
                    {
                        if (employee.Department.DepartmentTitle.ToLower().Contains(searchString.ToLower()))
                        {
                            Employees.Add(employee);
                        }
                    }
                }
            }
        }

        public ICommand ISelectPerson => new RelayCommand(selectPerson => SelectPerson());
        private void SelectPerson()
        {
            if(SelectedEmployee != null)
            {
                ExaminingPersonsWindow.Close();
            }
        }
    }
}
