using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public readonly IntranetDbContext _context;
        public readonly IWebHostEnvironment _environment;

        public DocumentController(IntranetDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentModel>>> GetDocument()
        {
            if (_context.Documents == null)
            {
                return NotFound();
            }
            return await _context.Documents.
                Select(x => new DocumentModel()
                {
                    ID = x.ID,
                    DocName = x.DocName,
                    DocSrc = String.Format("{0}://{1}{2}/Documents/{3}", Request.Scheme, Request.Host, Request.PathBase, x.DocName)

                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DocumentModel>> AddDocument([FromForm] DocumentModel document)
        {
            document.DocName = await UploadDocument(document.DocFile);
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocument), new { id = document.ID }, document);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentModel>> DeleteDocument(int id)
        {
            if(_context.Documents == null)
            {
                return NotFound();
            }
            var docModel = await _context.Documents.FindAsync(id);
            if(docModel == null)
            {
                return NotFound();
            }
            DeleteDocuments(docModel.DocName);
            _context.Documents.Remove(docModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [NonAction]
        public async Task<string> UploadDocument(IFormFile DocFile)
        {
            string DocName = new String(Path.GetFileNameWithoutExtension(DocFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            DocName = DocName + Path.GetExtension(DocFile.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "Documents", DocName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await DocFile.CopyToAsync(fileStream);
            }
            return DocName;
        }

        [NonAction]
        public void DeleteDocuments(string DocName)
        {
            var DocPath = Path.Combine(_environment.ContentRootPath, "Documents", DocName);
            if (System.IO.File.Exists(DocPath))
                System.IO.File.Delete(DocPath);
        }

    }
}
