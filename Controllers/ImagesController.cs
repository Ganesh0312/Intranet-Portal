using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntranetPortal.Models;
using Microsoft.Extensions.Hosting;

namespace IntranetPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IntranetDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImagesController(IntranetDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagesModel>>> GetImages()
        {
            if (_context.Images == null)
            {
                return NotFound();
            }

            return await _context.Images.
                Select(x => new ImagesModel()
                {
                    ID = x.ID,
                    ImageName = x.ImageName,
                    Imagesrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)

                }).ToListAsync();
        }
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ImagesModel>>> GetImages(string category)
        {
            IQueryable<ImagesModel> query = _context.Images;

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category == category);
            }              

            var images = await query.Select(x => new ImagesModel()
            {
                ID = x.ID,
                ImageName = x.ImageName,
                Imagesrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
            }).ToListAsync();

            return images;
        }
        [HttpPost("post")]
        public async Task<ActionResult<ImagesModel>> PostImagesModel([FromForm] ImagesModel imagesModel, string category)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set 'IntranetDbContext.Images' is null.");
            }

            imagesModel.ImageName = await SaveImage(imagesModel.ImageFile);
            imagesModel.Category = category; // Assign the category

            _context.Images.Add(imagesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImages), new { category = imagesModel.Category }, imagesModel);
        }

        [HttpPost]
        public async Task<ActionResult<ImagesModel>> PostImagesModel([FromForm] ImagesModel imagesModel)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set Images is null.");
            }
            imagesModel.ImageName = await SaveImage(imagesModel.ImageFile);
            _context.Images.Add(imagesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImages), new { id = imagesModel.ID }, imagesModel);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ImagesModel>> DeleteImagesModel(int id)
        {
            if (_context.Images == null)
            {
                return NotFound();
            }
            var imagesModel = await _context.Images.FindAsync(id);
            if (imagesModel == null)
            {
                return NotFound();
            }
            DeleteImage(imagesModel.ImageName);
            _context.Images.Remove(imagesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
         
        private bool ImagesModelExists(int id)
        {
            return (_context.Images?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
