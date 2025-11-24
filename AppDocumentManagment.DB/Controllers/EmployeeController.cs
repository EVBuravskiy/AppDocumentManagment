using AppDocumentManagment.DB.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;

namespace AppDocumentManagment.DB.Controllers
{
    public class EmployeeController
    {
        public bool AddEmployee(Employee newEmployee)
        {
            bool result = false;
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Employees.Add(newEmployee);
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (ApplicationContext context = new ApplicationContext())
            {
                employees = context.Employees.ToList();
            }
            return employees;
        }

        public Employee GetEmployeeByID(int employeeID)
        {
            Employee employee = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                employee = context.Employees.Where(e => e.EmployeeID == employeeID).FirstOrDefault();
            }
            return employee;
        }

        public List<Employee> GetEmployeesByDeparmentID(int departmentID)
        {
            List<Employee> employeesByDepartment = new List<Employee>();
            using (ApplicationContext context = new ApplicationContext())
            {
                employeesByDepartment = context.Employees.Where(x => x.DepartmentID == departmentID).ToList();
            }
            return employeesByDepartment;
        }

        public List<Employee> GetPerformers(int departmentID)
        {
            return GetEmployeesByDeparmentID(departmentID).Where(x => x.EmployeeRole == EmployeeRole.Performer).ToList();
        }

        public List<Employee> GetHeads(int departmentID)
        {
            List<Employee> DeputyGeneralDirectors = GetEmployeesByDeparmentID(departmentID).Where(x => x.EmployeeRole == EmployeeRole.DeputyGeneralDirector).ToList();
            List<Employee> HeadsOfDepartment = GetEmployeesByDeparmentID(departmentID).Where(x => x.EmployeeRole == EmployeeRole.HeadOfDepartment).ToList();
            List<Employee> result = new List<Employee>();
            result.AddRange(DeputyGeneralDirectors);
            result.AddRange(HeadsOfDepartment);
            return result;
        }

        public bool DeleteEmployee(Employee deletedEmployee)
        {
            bool result = false;
            using (ApplicationContext context = new ApplicationContext())
            {
                Employee currentEmployee = context.Employees.Where(x => x.EmployeeID == deletedEmployee.EmployeeID).FirstOrDefault();
                if (currentEmployee != null)
                {
                    context.Employees.Remove(currentEmployee);
                    context.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public bool UpdateEmployee(Employee inputEmployee)
        {
            bool result = false;
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Employees.Update(inputEmployee);
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        public Employee GetGeneralDirector()
        {
            Employee generalDirector = new Employee();
            using (ApplicationContext context = new ApplicationContext())
            {
                generalDirector = context.Employees.Where(e => e.EmployeeRole == EmployeeRole.GeneralDirector).FirstOrDefault();
            }
            return generalDirector;
        }

        public bool CheckAviableEmployee()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                List<Employee> employees = GetAllEmployees();
                if (employees.Count > 0) return true;
                return false;
            }
        }
    }
}
