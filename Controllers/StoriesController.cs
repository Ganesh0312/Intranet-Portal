using Intranet_Portal.Models;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        public readonly IntranetDbContext _context;
        public readonly IWebHostEnvironment _environment;
        public StoriesController(IntranetDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StriesModel>>> GetStories()
        {
            return await _context.Stories.
                 Select(x => new StriesModel()
                 {
                     Id = x.Id,
                     VedioName = x.VedioName,
                     VedioSrc = String.Format("{0}://{1}{2}/Strories/{3}", Request.Scheme, Request.Host, Request.PathBase, x.VedioName)

                 }).ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> UploadStrories([FromForm] StriesModel stori)
        {

            try
            {
                stori.VedioName = await SaveStories(stori.Vedio);
                await _context.Stories.AddAsync(stori);
                _context.SaveChanges();
                return Ok(new { Massage = "Stories Added Succesfully" });
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        public async Task<string> SaveStories(IFormFile videoFile)
        {
            string videoName = new String(Path.GetFileNameWithoutExtension(videoFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            videoName = videoName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(videoFile.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "Strories", videoName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(fileStream);
            }
            return videoName;
        }

        
    }
}
