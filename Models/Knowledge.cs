using System.ComponentModel.DataAnnotations;

namespace Intranet_Portal.Models
{
    public class Knowledge
    {
        public class DocumentHub
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string FileType { get; set; }
            public byte[] Content { get; set; }
            public int FolderId { get; set; }
            public Folder Folder { get; set; }
        }

        public class Folder
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsPublic { get; set; }
            public List<DocumentHub> Documents { get; set; }
        }
    }
}
