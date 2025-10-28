using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.UI.ViewModels
{
    public class ExaminingPersonViewModel : BaseViewModelClass
    {
        ExaminingPersonsWindow ExaminingPersonsWindow;

        private List<Employee> ManagersList {  get; set; }

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

        public ExaminingPersonViewModel(ExaminingPersonsWindow examiningPersonsWindow)
        {
            ExaminingPersonsWindow = examiningPersonsWindow;
            ManagersList = new List<Employee>();
            Managers = new ObservableCollection<Employee>();
            GetManagersList();
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
        private void InitializeManagers()
        {
            Managers.Clear();
            if (ManagersList.Count > 0)
            {
                ManagersList.Sort((m1, m2) => m1.DepartmentID.CompareTo(m2.DepartmentID));
                foreach (Employee manager in ManagersList)
                {
                    Managers.Add(manager);
                }
            }
        }
    }
}
