using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntranetPortal.Models;

namespace IntranetPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IntranetDbContext _context;

        public NewsController(IntranetDbContext context)
        {
            _context = context;
        }

        // GET: api/News
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsModel>>> GetNews()
        {
          if (_context.News == null)
          {
              return NotFound();
          }
            return await _context.News.ToListAsync();
        }

       /* // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsModel>> GetNewsModel(int id)
        {
          if (_context.News == null)
          {
              return NotFound();
          }
            var newsModel = await _context.News.FindAsync(id);

            if (newsModel == null)
            {
                return NotFound();
            }

            return newsModel;
        }

        // PUT: api/News/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsModel(int id, NewsModel newsModel)
        {
            if (id != newsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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

            return NoContent();
        }*/

    
        [HttpPost]
        public async Task<ActionResult<NewsModel>> PostNewsModel([FromForm]NewsModel newsModel)
        {
          if (_context.News == null)
          {
              return Problem("Entity set 'IntranetDbContext.News'  is null.");
          }
            newsModel.Imagesrc = await UploadImage(newsModel.Image);
            _context.News.Add(newsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNews), new { id = newsModel.Id }, newsModel);
        }

        [NonAction]
        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"Images", special + "-" + file.FileName);
            using (FileStream ms = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            var filename = special + "-" + file.FileName;
            return filepath;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsModel(int id)
        {
            if (_context.News == null)
            {
                return NotFound();
            }
            var newsModel = await _context.News.FindAsync(id);
            if (newsModel == null)
            {
                return NotFound();
            }

            _context.News.Remove(newsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       /* private bool NewsModelExists(int id)
        {
            return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
