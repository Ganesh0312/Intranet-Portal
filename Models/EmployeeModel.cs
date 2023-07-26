
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public class EmployeeModel
    {
        [Key]
        public int employeesID { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string employeeName { get; set; }
        public string mail { get; set; }
        public string mobile { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime dob { get; set; } 
        public DateTime dateOfJoin { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string password { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? imageName { get; set; }

        [NotMapped]
        public IFormFile imageFile { get; set; }

        [NotMapped]
        public string? imageSrc { get; set; }
    }
}
