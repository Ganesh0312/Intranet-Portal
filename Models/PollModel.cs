using System.ComponentModel.DataAnnotations;

namespace Intranet_Portal.Models
{
    public class PollModel
    {
        public class Poll
        {
            [Key]
            public int Id { get; set; }
            public string Question { get; set; }
            public List<string> Options { get; set; }

            public PollResults Results { get; set; }
        }

        public class PollResults
        {
            [Key]
            public int Id { get; set; }
            public int PollId { get; set; }
            public Dictionary<string, int> Results { get; set; }

            // Other properties or relationships can be added here
        }
    }
}
