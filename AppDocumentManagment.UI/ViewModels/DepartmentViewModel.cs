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

        public ICommand ICanselEditing => new RelayCommand(canselEditing => CanselEditing());

        private void CanselEditing()
        {
            DepartmentTitle = "";
            DepartmentShortTitle = "";
        }

        public ICommand ISave => new RelayCommand(save => Save());
        private void Save()
        {
            if (string.IsNullOrEmpty(DepartmentTitle)) return;
            if (string.IsNullOrEmpty(DepartmentShortTitle)) return;
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

        public ICommand IExit => new RelayCommand(exit => Exit());
        private void Exit()
        {
            DepartmentTitle = "";
            DepartmentShortTitle = "";
            DepartmentWindow.Close();
        }
    }
}
