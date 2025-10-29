using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagment.UI.ViewModels
{
    public class DepartmentViewModel : BaseViewModelClass
    {
        private DepartmentWindow DepartmentWindow;

        private Department _selectedDepartment = null;

        private string _departmentTitle = "";
        public string DepartmentTitle
        {
            get => _departmentTitle;
            set
            {
                _departmentTitle = value;
                OnPropertyChanged(nameof(DepartmentTitle));
            }
        }

        private string _departmentShortTitle = "";
        public string DepartmentShortTitle
        {
            get => _departmentShortTitle;
            set
            {
                _departmentShortTitle = value;
                OnPropertyChanged(nameof(DepartmentShortTitle));
            }
        }

        public DepartmentViewModel(DepartmentWindow departmentWindow, Department inputDepartment)
        {
            DepartmentWindow = departmentWindow;
            _selectedDepartment = inputDepartment;
            if (inputDepartment != null)
            {
                DepartmentTitle = _selectedDepartment.DepartmentTitle;
                DepartmentShortTitle = _selectedDepartment.DepartmentShortTitle;
            }
        }

        public ICommand IDelete => new RelayCommand(delete => Delete());

        private void Delete()
        {
            bool result = false;
            if (_selectedDepartment != null)
            {
                EmployeeController employeeController = new EmployeeController();
                List<Employee> employeesDepartment = employeeController.GetEmployeesByDeparmentID(_selectedDepartment.DepartmentID);
                foreach (Employee employee in employeesDepartment)
                {
                    employeeController.DeleteEmployee(employee);
                }
                DepartmentController departmentController = new DepartmentController();
                result = departmentController.RemoveDepartment(_selectedDepartment);
            }
            if (result)
            {
                MessageBox.Show($"Удаление отдела {DepartmentTitle} выполнено успешно");
                DepartmentTitle = "";
                DepartmentShortTitle = "";
                return;
            }
            MessageBox.Show($"Ошибка! Удаление отдела {DepartmentTitle} не выполнено");
            DepartmentWindow.Close();
        }

        public ICommand ISave => new RelayCommand(save => Save());
        private void Save()
        {
            if (!ValidateDepatment()) return;
            bool result = false;
            if (_selectedDepartment == null)
            {
                Department newDepartment = new Department();
                newDepartment.DepartmentTitle = DepartmentTitle;
                newDepartment.DepartmentShortTitle = DepartmentShortTitle;
                DepartmentController controller = new DepartmentController();
                result = controller.AddDepartment(newDepartment);
            }
            else
            {
                _selectedDepartment.DepartmentTitle = DepartmentTitle;
                _selectedDepartment.DepartmentShortTitle = DepartmentShortTitle;
                DepartmentController controller = new DepartmentController();
                result = controller.UpdateDepartment(_selectedDepartment);
            }
            if (result)
            {
                MessageBox.Show($"Сохранение отдела {DepartmentTitle} выполнено успешно");
                DepartmentTitle = "";
                DepartmentShortTitle = "";
                return;
            }
            MessageBox.Show($"Ошибка! Сохранение отдела {DepartmentTitle} не выполнено");
        }

        private bool ValidateDepatment()
        {
            if (string.IsNullOrEmpty(DepartmentTitle))
            {
                MessageBox.Show("Введите наименование отдела/департамента");
                DepartmentWindow.DepartmentTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                DepartmentWindow.DepartmentTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            if (string.IsNullOrEmpty(DepartmentShortTitle))
            {
                MessageBox.Show("Введите сокращенное наименование отдела/департамента");
                DepartmentWindow.DepartmentShortTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentShortTitle.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                DepartmentWindow.DepartmentShortTitle.BorderThickness = new System.Windows.Thickness(2);
                DepartmentWindow.DepartmentShortTitle.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            return true;
        }

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            DepartmentTitle = "";
            DepartmentShortTitle = "";
            DepartmentWindow.Close();
        }
    }
}
