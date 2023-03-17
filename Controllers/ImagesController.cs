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
    public class ImagesController : ControllerBase
    {
        private readonly IntranetDbContext _context;

        public ImagesController(IntranetDbContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagesModel>>> GetImages()
        {
          if (_context.Images == null)
          {
              return NotFound();
          }
            return await _context.Images.ToListAsync();
        }

        // GET: api/Images/5
       /* [HttpGet("{id}")]
        public async Task<ActionResult<ImagesModel>> GetImagesModel(int id)
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

            return imagesModel;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagesModel(int id, ImagesModel imagesModel)
        {
            if (id != imagesModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(imagesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagesModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754*/
       
        [HttpPost]
        public async Task<ActionResult<ImagesModel>> PostImagesModel([FromForm]ImagesModel imagesModel)
        {
          if (_context.Images == null)
          {
              return Problem("Entity set 'IntranetDbContext.Images'  is null.");
          }
            imagesModel.Imagesrc = await UploadImage(imagesModel.Image);
            _context.Images.Add(imagesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImages), new { id = imagesModel.ID }, imagesModel);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagesModel(int id)
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

            _context.Images.Remove(imagesModel);
            await _context.SaveChangesAsync();

            return NoContent();
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

        private bool ImagesModelExists(int id)
        {
            return (_context.Images?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
