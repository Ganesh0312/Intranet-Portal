using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntranetPortal.Models
{
    public class DocumentModel
    {
        [Key]
        public int DocID { get; set; }

        public string? DocName { get; set; }
        [NotMapped]
        public IFormFile DocFile { get; set; }
        [NotMapped]
        public string? DocSrc { get; set; }

    }
}
