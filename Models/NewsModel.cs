using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public class NewsModel
    {
        [Key]
        public int newsId { get; set; }

        public string newsTitale { get; set; }

        public String content { get; set; }

        public string? imageName { get; set; }
        [NotMapped]
        public IFormFile imageFile { get; set; }
        [NotMapped]
        public string? imageUrl { get; set; }

    }
}
