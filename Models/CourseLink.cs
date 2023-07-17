using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet_Portal.Models
{
    public class CourseLink
    {
       
            [Key]
            public int videoId { get; set; }
            public string videoURL { get; set; }
            [NotMapped]
            public string? videoTitle { get; set; }
        }
    }
