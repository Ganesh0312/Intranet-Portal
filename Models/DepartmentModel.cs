using System.ComponentModel.DataAnnotations;

namespace IntranetPortal.Models
{
    public class DepartmentModel
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}
