using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.UI.Utilities
{
    public class DepartmentConverter
    {
        public static string DepartmentToString(Department department)
        {
            return department.DepartmentTitle;
        }

        public static Department StringToDepartment(string departmentTitle)
        {
            DepartmentController controller = new DepartmentController();
            Department department = controller.GetDepartmentByTitle(departmentTitle);
            return department;
        }

        public static int DepartmentToInt(Department department, List<Department> departments)
        {
            for (int i = 0; i < departments.Count; i++)
            {
                if (departments[i].DepartmentID == department.DepartmentID)
                {
                    return i;
                }
            }
            return 0;
        }

        public static Department IntToDepartment(int index, List<Department> departments)
        {
            return departments[index];
        }
    }
}
