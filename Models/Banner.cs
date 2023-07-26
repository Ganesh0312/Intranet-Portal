using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet_Portal.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
       
    }
}
