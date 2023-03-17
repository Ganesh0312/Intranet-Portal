using System.ComponentModel.DataAnnotations;

namespace IntranetPortal.Models
{
    public class DepartmentModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}
