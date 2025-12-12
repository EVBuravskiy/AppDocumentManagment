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
        public DbSet<EmployeePhoto> EmployeePhotos { get; set; } = null!;
        public DbSet<RegistredUser> RegistredUsers { get; set; } = null!;
        public DbSet<ContractorCompany> ContractorCompanies { get; set; } = null!;
        public DbSet<ExternalDocument> ExternalDocuments { get; set; } = null!;
        public DbSet<ExternalDocumentFile> ExternalDocumentFiles { get; set; } = null!;
        public DbSet<InternalDocument> InternalDocuments { get; set; } = null!;
        public DbSet<InternalDocumentFile> InternalDocumentFiles { get; set; } = null!;
        public DbSet<ProductionSubTask> ProductionSubTasks { get; set; } = null!;
        public DbSet<ProductionTaskComment> ProductionTaskComments { get; set; } = null!;
        public DbSet<ProductionTaskFile> ProductionTaskFiles { get; set; } = null!;
        public DbSet<ProductionTask> ProductionTasks { get; set; } = null!;
        public ApplicationContext() 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CIIC8VG;Initial Catalog=AppDocumentsManagerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
