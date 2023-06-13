using IntranetPortal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet_Portal.Models
{
    public class EscalationMatrix
    {
        [Key]
        public int Id { get; set; }
        public string Topic { get; set; }
        public int EmployeeId { get; set; }
    }
}
