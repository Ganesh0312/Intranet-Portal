using System.ComponentModel.DataAnnotations;

namespace Intranet_Portal.Models
{
    public class Poll
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        

        

    }
}
