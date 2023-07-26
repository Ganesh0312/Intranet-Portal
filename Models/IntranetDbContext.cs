using Microsoft.EntityFrameworkCore;
using IntranetPortal.Models;
using Intranet_Portal.Models;

namespace IntranetPortal.Models
{
    public class IntranetDbContext :DbContext
    {
        public IntranetDbContext(DbContextOptions<IntranetDbContext> options) : base(options) 
        {
            
        }
        public DbSet<EmployeeModel> EmployeesModel { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<DesignationModel> Designations { get; set; }
        public DbSet<ImagesModel> Images { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }
        public DbSet<NewsModel> NewsModels { get; set; }
        public DbSet<MotivationModel> Motivations { get; set; }
        public DbSet<StriesModel> Stories { get; set; }
        public DbSet<KnowledgeHub> KnowledgeHubs { get; set; }
        public DbSet<Poll> polls { get; set; } 
        public DbSet<Vote> Votes { get; set; }
        public DbSet<CourseLink> CourseLinks { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<EscalationMatrix> Escalations { get; set; }

       

    }
}
