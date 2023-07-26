using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet_Portal.Models
{
    public class KnowledgeHub
    {

        public int ID { get; set; }
        public string Category { get; set; }

        [NotMapped]
        public IFormFile DocFile { get; set; }
        public string? DocName { get; set; }
        public string? DocSrc { get; set; }
    }
}
