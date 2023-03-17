using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public class DesignationModel
    {
        
            [Key]
            public int ID { get; set; }
           
            [Required]
            public string DesignationName { get; set; }
       
    }
}
