using AppDocumentManagment.DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppDocumentManagment.DB.Controllers
{
    public class DepartmentController
    {
        public bool AddDepartment(Department department)
        {
            bool result = false;
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Add(department);
                context.SaveChanges();
                result = true;
            }
            return result;
        }
        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();
            using (ApplicationContext context = new ApplicationContext())
            {
                departments = context.Departments.ToList();
            }
            return departments;
        }
        public Department GetDepartmentByID(int id)
        {
            Department department = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                department = context.Departments.FirstOrDefault(x => x.DepartmentID == id);
            }
            return department;
        }

        public Department GetDepartmentByTitle(string title)
        {
            Department department = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                department = context.Departments.FirstOrDefault(x => x.DepartmentTitle == title);
                if (department == null)
                {
                    department = context.Departments.FirstOrDefault(x => x.DepartmentTitle.Contains(title));
                }
            }
            if (department == null)
            {
                department = GetDepartmentByShortTitle(title);
            }
            return department;
        }

        public Department GetDepartmentByShortTitle(string shortTitle)
        {
            Department department = null;
            using (ApplicationContext context = new ApplicationContext())
            {
                department = context.Departments.FirstOrDefault(x => x.DepartmentShortTitle == shortTitle);
                if (department == null)
                {
                    department = context.Departments.FirstOrDefault(x => x.DepartmentShortTitle.Contains(shortTitle));
                }
            }
            return department;
        }

        public bool UpdateDepartment(Department inputDepartment)
        {
            bool result = false;
            using (ApplicationContext context = new ApplicationContext())
            {
                Department currentDepartment = context.Departments.FirstOrDefault(x => x.DepartmentID == inputDepartment.DepartmentID);
                if (currentDepartment == null)
                {
                    result = AddDepartment(inputDepartment);
                    return result;
                }
                currentDepartment.DepartmentTitle = inputDepartment.DepartmentTitle;
                currentDepartment.DepartmentShortTitle = inputDepartment.DepartmentShortTitle;
                context.Departments.Update(currentDepartment);
                context.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool RemoveDepartment(Department inputDepartment)
        {
            bool result = false;
            using (ApplicationContext context = new ApplicationContext())
            {
                Department currentDepartment = context.Departments.FirstOrDefault(x => x.DepartmentID == inputDepartment.DepartmentID);
                if (currentDepartment == null)
                {
                    return result;
                }
                context.Departments.Remove(currentDepartment);
                context.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}

