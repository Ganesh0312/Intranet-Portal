using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public class ImagesModel
    {
        [Key]
        public int ID { get; set; }
        public string? Category { get; set; }
        [NotMapped]
        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? Imagesrc { get; set; }

    }
}
