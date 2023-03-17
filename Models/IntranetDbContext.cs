using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Models
{
    public class IntranetDbContext :DbContext
    {
        public IntranetDbContext(DbContextOptions<IntranetDbContext> options) : base(options) 
        {
            
        }
        public DbSet<EmployeeModel> Employees { get; set; }

        public DbSet<DepartmentModel> Departments { get; set; }

        public  DbSet<DesignationModel> Designations { get; set; }

        public DbSet<ImagesModel> Images { get; set; }

       // public DbSet<DocumentModel> Documents { get; set; }

        public DbSet<NewsModel> News { get; set; }

        public DbSet<MotivationModel> Motivations { get; set; }

    }
}
