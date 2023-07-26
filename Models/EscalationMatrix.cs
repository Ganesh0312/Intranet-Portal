using IntranetPortal.Models;
using System.ComponentModel.DataAnnotations;

namespace Intranet_Portal.Models
{
    public class EscalationMatrix
    {
        [Key]
        public int Id { get; set; }
        public string TopicName { get; set; }
        public string ResponsibleEmployees { get; set; }
    }

}
