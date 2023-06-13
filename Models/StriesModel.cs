using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet_Portal.Models
{
    public class StriesModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Vedio { get; set; }
        public string? VedioName { get; set; }
        public string? VedioSrc { get; set; }
    }
} 