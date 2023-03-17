using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IntranetPortal.Models
{
    public class EmployeeModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Age { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Mail { get; set; }

        public string DOB { get; set; }

        public string JoiningDate { get; set; }
        
        public string Designation { get; set; }
        
        public string Department { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
    }

    

   
}
