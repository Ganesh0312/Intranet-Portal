using Microsoft.EntityFrameworkCore;
using IntranetPortal.Models;
using Intranet_Portal.Models;
using System.Reflection.Metadata;
using static Intranet_Portal.Models.Knowledge;

namespace IntranetPortal.Models
{
    public class IntranetDbContext :DbContext
    {
        public IntranetDbContext(DbContextOptions<IntranetDbContext> options) : base(options) 
        {
            
        }
        public DbSet<EmployeeModel> EmployeesModel { get; set; }

        public DbSet<DepartmentModel> Departments { get; set; }

        public  DbSet<DesignationModel> Designations { get; set; }

        public DbSet<ImagesModel> Images { get; set; }

        public DbSet<DocumentModel> Documents { get; set; }

        public DbSet<NewsModel> NewsModels { get; set; }

        public DbSet<MotivationModel> Motivations { get; set; }
        public DbSet<StriesModel> Stories { get; set; }

        public DbSet<EscalationMatrix> EMatrix { get; set; }

        public DbSet<DocumentHub> DocumentsHubs { get; set; }
        public DbSet<Folder> Folders { get; set; }

        public DbSet<PollModel> Polls { get; set; }
        
    }
}
