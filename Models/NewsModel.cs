using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public class NewsModel
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime NewsDate { get; set; }

        public string News { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? Imagesrc { get; set; }

    }
}
