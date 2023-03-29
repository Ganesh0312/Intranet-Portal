using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using IntranetPortal.Models;

namespace IntranetPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IntranetDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(IntranetDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsModel>>> GetNews()
        {
            return await _context.NewsModels.Select(x => new NewsModel
            {
                newsId = x.newsId,
                newsTitale =x.newsTitale,
                content = x.content,
                imageName = x.imageName,
                imageUrl = String.Format("{0}://{1}{2}/NewsImage/{3}", Request.Scheme, Request.Host, Request.PathBase, x.imageName)

            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsModel>> GetNewsById(int id)
        {
            var newsModel =await _context.NewsModels.FindAsync(id);
            if(newsModel == null)
            {
               return NotFound();
            }
            return newsModel;
        }

        [HttpPost]
        public async Task<ActionResult<NewsModel>> AddNews([FromForm] NewsModel newsModel)
        {
            newsModel.imageName = await SaveImage(newsModel.imageFile);
            _context.NewsModels.Add(newsModel);
            await _context.SaveChangesAsync();

            return Ok(newsModel);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NewsModel>> EditNews(int id, [FromForm] NewsModel newsModel)
        {
            if(id !=newsModel.newsId)
            {
                return BadRequest();
            }

            if(newsModel.imageName == null)
            {
                DeleteImage(newsModel.imageName);
                newsModel.imageName = await SaveImage(newsModel.imageFile);
            }

            _context.Entry(newsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!NewsModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(newsModel);
            
        }

        [HttpDelete()]
        public async Task<ActionResult<NewsModel>> DeleteNews(int id)
        {
            var newsmodel = await _context.NewsModels.FindAsync(id);
            if (newsmodel == null)
            {
                return BadRequest();
            }
            DeleteImage(newsmodel.imageName);
             _context.NewsModels.Remove(newsmodel);
            await _context.SaveChangesAsync();
            return Ok(newsmodel);
        } 




        private bool NewsModelExists(int id)
        {
            return _context.NewsModels.Any(e => e.newsId == id);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "NewsImage", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
        /* [NonAction]
         public async Task<string> UploadImage(IFormFile file)
         {
             var special = Guid.NewGuid().ToString();
             var filepath = Path.Combine(_hostEnvironment.ContentRootPath, @"NewsImage", special + "-" + file.FileName);
             using (FileStream ms = new FileStream(filepath, FileMode.Create))
             {
                 await file.CopyToAsync(ms);
             }
            // var filename = special + "-" + file.FileName;
             return filepath;
         }*/

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "NewsImage", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
