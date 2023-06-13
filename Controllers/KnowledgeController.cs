using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using static Intranet_Portal.Models.Knowledge;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        public readonly IntranetDbContext _context;

        public KnowledgeController(IntranetDbContext context)
        {
            _context = context;
        }

        [HttpPost("Folder")]
        public async Task<IActionResult> CreateFolder([FromBody] Folder folder)
        {
            
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Folder created successfully" });
        }

        [HttpPost("Document")]
        public async Task<IActionResult> UploadDocument([FromForm] IFormFile file, int folderId)
        {
            // Assuming _context is the database context
            var folder = await _context.Folders.FindAsync(folderId);

            if (folder == null)
            {
                return NotFound(new { Message = "Folder not found" });
            }

            if (file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    var document = new DocumentHub
                    {
                        Name = file.FileName,
                        FileType = file.ContentType,
                        Content = stream.ToArray(),
                        FolderId = folderId
                    };
                     _context.DocumentsHubs.Add(document);
                    await _context.SaveChangesAsync();
                }
            }
            return Ok(new { Message = "Document uploaded successfully" });
        }
    }
}
