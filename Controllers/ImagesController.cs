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

        public ImagesController(IntranetDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IEnumerable<ImagesModel>> GetImages()
        {
            return await _context.Images
                .Select(x => new ImagesModel(){
                ID= x.ID,
                ImageName=x.ImageName,
                Imagesrc = String.Format("{0}://{1}{2}/Image/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ImagesModel>> AddImages([FromForm] ImagesModel imagesmodel)
        {
            imagesmodel.ImageName = await SaveImage(imagesmodel.ImageFile);
            _context.Images.Add(imagesmodel);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Images Added Completed" });

        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Image", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
    }
