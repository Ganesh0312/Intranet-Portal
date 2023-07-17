using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntranetPortal.Models;
using Intranet_Portal.Models;
using Microsoft.CodeAnalysis;

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

        /*        [HttpGet]
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
                }*/


        [HttpGet("Content")]
        public async Task<ActionResult<IEnumerable<KnowledgeHub>>> GetDocument()
        {
            if (_context.KnowledgeHubs == null)
            {
                return NotFound();
            }
            return await _context.KnowledgeHubs.
                Select(x => new KnowledgeHub()
                {
                    ID = x.ID,
                    DocName = x.DocName,
                    Category = x.Category,
                    DocSrc = String.Format("{0}://{1}{2}/Knowledge/{3}", Request.Scheme, Request.Host, Request.PathBase, x.DocName)
                }).ToListAsync();
        }

        [HttpPost("Content")]
        public async Task<ActionResult<KnowledgeHub>> AddDocument([FromForm] KnowledgeHub document)
        {
            document.DocName = await UploadContent(document.DocFile);
            _context.KnowledgeHubs.Add(document);
            await _context.SaveChangesAsync();

            return Ok();
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
            DeleteContent(docModel.DocName);
            _context.KnowledgeHubs.Remove(docModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [NonAction]
        public async Task<string> UploadContent(IFormFile DocFile)
        {
            string DocName = new String(Path.GetFileNameWithoutExtension(DocFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            DocName = DocName + Path.GetExtension(DocFile.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "Knowledge", DocName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await DocFile.CopyToAsync(fileStream);
            }
            return DocName;
        }

        [NonAction]
        public void DeleteContent(string DocName)
        {
            var DocPath = Path.Combine(_environment.ContentRootPath, "Documents", DocName);
            if (System.IO.File.Exists(DocPath))
                System.IO.File.Delete(DocPath);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseLink>>> GetYStory()
        {
            return await _context.CourseLinks.Select(x => new CourseLink
            {
                videoId = x.videoId,
                videoURL = x.videoURL,
                videoTitle = x.videoTitle
                //String.Format("{0}://{1}{2}/VideoTitale/{3}", Request.Scheme, Request.Host, Request.PathBase, x.videoTitale)

            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseLink>> GetStriesById(int id)
        {
            var ystoriesModel = await _context.CourseLinks.FindAsync(id)
;
            if (ystoriesModel == null)
            {
                return NotFound();
            }
            return ystoriesModel;
        }

        [HttpPost("Courses")]
        public async Task<ActionResult<CourseLink>> AddStries([FromForm] CourseLink ystoriesModel)
        {
            // striesModel.imageName = await SaveImage(striesModel.imageFile);
            _context.CourseLinks.Add(ystoriesModel);
            await _context.SaveChangesAsync();

            return Ok(ystoriesModel);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseLink>> EditVideo(int id, [FromForm] CourseLink ystoriesModel)
        {
            if (id != ystoriesModel.videoId)
            {
                return BadRequest();
            }


            _context.Entry(ystoriesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YStoriesModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(ystoriesModel);

        }

        [HttpDelete]
        public async Task<ActionResult<CourseLink>> DeleteStries(int id)
        {
            var ystoriesmodel = await _context.CourseLinks.FindAsync(id)
;
            if (ystoriesmodel == null)
            {
                return BadRequest();
            }

            _context.CourseLinks.Remove(ystoriesmodel);
            await _context.SaveChangesAsync();
            return Ok(ystoriesmodel);
        }

        private bool YStoriesModelExists(int id)
        {
            return _context.CourseLinks.Any(e => e.videoId == id);
        }


    }
}
