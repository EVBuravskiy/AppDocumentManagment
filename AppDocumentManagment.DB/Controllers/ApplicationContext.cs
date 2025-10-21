using AppDocumentManagment.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace AppDocumentManagment.DB.Controllers
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<RegistredUser> RegistredUsers { get; set; } = null!;
        public ApplicationContext() 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CIIC8VG;Initial Catalog=AppDocumentsFlowDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
