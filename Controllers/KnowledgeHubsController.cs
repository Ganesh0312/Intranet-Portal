using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntranetPortal.Models;
using Intranet_Portal.Models;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeHubsController : ControllerBase
    {
        public readonly IntranetDbContext _context;
        public readonly IWebHostEnvironment _environment;

        public KnowledgeHubsController(IntranetDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnowledgeHub>>> GetDocument()
        {
            if (_context.KnowledgeHubs == null)
            {
                return NotFound();
            }

            return await _context.KnowledgeHubs
                .Select(x => new KnowledgeHub()
                {
                    ID = x.ID,
                    DocName = x.DocName,
                    DocSrc = GetDocumentUrl(this,x.DocName)
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<KnowledgeHub>> AddDocument([FromForm] KnowledgeHub document)
        {
            document.DocName = await UploadDocument(document.DocFile, document.FolderName);
            _context.KnowledgeHubs.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocument), new { id = document.ID }, document);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<KnowledgeHub>> DeleteDocument(int id)
        {
            if (_context.KnowledgeHubs == null)
            {
                return NotFound();
            }

            var docModel = await _context.KnowledgeHubs.FindAsync(id);
            if (docModel == null)
            {
                return NotFound();
            }

            DeleteDocumentFile(docModel.DocName, docModel.FolderName);
            _context.KnowledgeHubs.Remove(docModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [NonAction]
        public async Task<string> UploadDocument(IFormFile docFile, string folderName)
        {
            string docName = Path.GetFileNameWithoutExtension(docFile.FileName).Replace(' ', '-');
            string uniqueFileName = GetUniqueFileName(docName);
            string folderPath = Path.Combine(_environment.ContentRootPath, "Knowledge", folderName);
            string filePath = Path.Combine(folderPath, uniqueFileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await docFile.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        [NonAction]
        public void DeleteDocumentFile(string docName, string folderName)
        {
            string folderPath = Path.Combine(_environment.ContentRootPath, "Knowledge", folderName);
            string filePath = Path.Combine(folderPath, docName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);

                // Delete the folder if it becomes empty after deleting the file
                if (!Directory.EnumerateFiles(folderPath).Any())
                {
                    Directory.Delete(folderPath);
                }
            }
        }

        [NonAction]
        public static string GetDocumentUrl(ControllerBase controller, string docName)
        {
            return controller.Url.Content("~/Knowledge/" + docName);
        }

        [NonAction]
        public string GetUniqueFileName(string fileName)
        {
            fileName = new string(fileName.Take(10).ToArray());
            fileName = fileName +  Path.GetExtension(fileName);

            string uniqueFileName = fileName;
            int counter = 1;

            while (System.IO.File.Exists(Path.Combine(_environment.ContentRootPath, "Knowledge", uniqueFileName)))
            {
                uniqueFileName = $"{fileName}-{counter++}{Path.GetExtension(fileName)}";
            }

            return uniqueFileName;
        }

    }
}
