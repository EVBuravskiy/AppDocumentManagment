using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.DB.Controllers
{
    public class EmployeeController
    {
        public void AddEmployee(Employee newEmployee)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Employees.Add(newEmployee);
                context.SaveChanges();
            }
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

        public void UpdateEmployee(Employee inputEmployee)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Employees.Update(inputEmployee);
                context.SaveChanges();
            }
        }
    }
}
