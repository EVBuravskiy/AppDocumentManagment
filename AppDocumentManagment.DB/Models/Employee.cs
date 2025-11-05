namespace AppDocumentManagment.DB.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? EmployeeFirstName { get; set; }
        public string? EmployeeLastName { get; set; }
        public string? EmployeeMiddleName { get; set; }
        public Department Department { get; set; }
        public int DepartmentID { get; set; }
        public string? Position { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public string EmployeeImagePath { get; set; }
        public string EmployeeFullName => $"{EmployeeLastName} {EmployeeFirstName} {EmployeeMiddleName}";
        public string EmployeeShortName => $"{EmployeeLastName} {EmployeeFirstName.ElementAt(0)}.{EmployeeMiddleName.ElementAt(0)}.";
        public List<Document> Documents { get; set; } = new List<Document>();
        public List<InternalDocument> InternalDocuments { get; set; } = new List<InternalDocument>();
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeInformation { get; set; }
    }
}
