using Intranet_Portal.Models;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        readonly IntranetDbContext _context;
        readonly IWebHostEnvironment _environment;

        public BannerController(IntranetDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banner>>> GetBanner()
        {
            if (_context.Documents == null)
            {
                return NotFound();
            }
            return await _context.Banners.
                Select(x => new Banner()
                {
                    Id = x.Id,
                    ImageName = x.ImageName,
                    ImageUrl = String.Format("{0}://{1}{2}/Banner/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)

                }).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Banner>> AddBanner([FromForm] Banner banner)
        {
            banner.ImageName = await UploadBanner(banner.ImageFile);
            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Banner Added Successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Banner>> DeleteBanner(int id)
        {
            if (_context.Banners == null)
            {
                return NotFound();
            }
            var banModel = await _context.Banners.FindAsync(id);
            if (banModel == null)
            {
                return NotFound();
            }
            DeleteBanner(banModel.ImageName);
            _context.Banners.Remove(banModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [NonAction]
        public async Task<string> UploadBanner(IFormFile ImageFile)
        {
            string DocName = new String(Path.GetFileNameWithoutExtension(ImageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            DocName = DocName + Path.GetExtension(ImageFile.FileName);
            var imagePath = Path.Combine(_environment.ContentRootPath, "Banner", DocName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return DocName;
        }

        [NonAction]
        public void DeleteBanner(string DocName)
        {
            var DocPath = Path.Combine(_environment.ContentRootPath, "Banner", DocName);
            if (System.IO.File.Exists(DocPath))
                System.IO.File.Delete(DocPath);
        }
    }
}
