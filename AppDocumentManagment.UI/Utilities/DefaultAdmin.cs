using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using System.Windows;


namespace AppDocumentManagment.UI.Utilities
{
    public class DefaultAdmin
    {
        public static bool CreateDefaultAdmin()
        {
            Department defaultDepartment = CreateDefaultDepartment();
            Employee defaultEmployee = CreateDefaultEmployee(defaultDepartment);
            bool result = RegistredDefaultAdmin(defaultEmployee);
            if (result)
            {
                MessageBox.Show("Создан администратор по умолчанию");
                return true;
            }
            else
            {
                MessageBox.Show("Не удалось создать дефолтного администратора");
                return false;
            }
        }

        private static Department CreateDefaultDepartment()
        {
            Department defaultDepartment = new Department();
            defaultDepartment.DepartmentTitle = "Default";
            defaultDepartment.DepartmentShortTitle = "Default";
            DepartmentController departmentController = new DepartmentController();
            bool result = departmentController.AddDepartment(defaultDepartment);
            if (!result)
            {
                MessageBox.Show("Ошибка создания дефолтного отдела");
                return null;
            }
            return defaultDepartment;
        }

        private static Employee CreateDefaultEmployee(Department defaultDepartment)
        {
            Employee defaultEmployee = new Employee();
            defaultEmployee.EmployeeFirstName = "Default";
            defaultEmployee.EmployeeLastName = "Default";
            defaultEmployee.EmployeeMiddleName = "Default";
            DepartmentController departmentController = new DepartmentController();
            Department department = departmentController.GetDepartmentByTitle(defaultDepartment.DepartmentTitle);
            defaultEmployee.DepartmentID = department.DepartmentID;
            EmployeeController employeeController = new EmployeeController();
            employeeController.AddEmployee(defaultEmployee);
            return defaultEmployee;
        }

        private static bool RegistredDefaultAdmin(Employee defaultEmployee)
        {
            RegistredUser registredUser = new RegistredUser();
            registredUser.RegistredUserLogin = "Admin01";
            registredUser.RegistredUserTime = DateTime.Now;
            string password = $"{registredUser.RegistredUserLogin}-01Nimda";
            registredUser.RegistredUserPassword = PassHasher.CalculateMD5Hash(password);
            registredUser.IsRegistered = true;
            EmployeeController employeeController = new EmployeeController();
            Employee employee = employeeController.GetEmployeeByID(defaultEmployee.EmployeeID);
            registredUser.EmployeeID = employee.EmployeeID;
            registredUser.UserRole = UserRole.Administrator;
            RegistredUserController registredUserController = new RegistredUserController();
            return registredUserController.AddRegistratedUser(registredUser);
        }
    }
}
