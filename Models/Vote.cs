using System.ComponentModel.DataAnnotations;

namespace Intranet_Portal.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        public int PollId { get; set; }
        public int OptionNumber { get; set; }
    }
}
