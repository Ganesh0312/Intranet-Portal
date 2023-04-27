using System.ComponentModel.DataAnnotations;

namespace IntranetPortal.Models
{
    public class MotivationModel
    {
        [Key]
        public int Id { get; set; }
        public string Motivation { get; set; }

    }
}
