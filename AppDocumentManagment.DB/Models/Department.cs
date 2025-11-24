using System.Collections.Generic;

namespace AppDocumentManagment.DB.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentTitle { get; set; }
        public string DepartmentShortTitle { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
