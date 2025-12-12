namespace AppDocumentManagment.DB.Models
{
    public class RegistredUser
    {
        public int RegistredUserID { get; set; }
        public string RegistredUserLogin { get; set; }
        public string RegistredUserPassword { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime RegistredUserTime { get; set; }
        public Employee? Employee { get; set; }
        public int EmployeeID { get; set; }
        public bool IsRegistered { get; set; } = false;
    }
}
