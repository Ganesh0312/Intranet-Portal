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
    public class MotivationController : ControllerBase
    {
        private readonly IntranetDbContext _context;

        public MotivationController(IntranetDbContext context)
        {
            _context = context;
        }

        // GET: api/Motivation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotivationModel>>> GetMotivations()
        {
          if (_context.Motivations == null)
          {
              return NotFound();
          }
            return await _context.Motivations.ToListAsync();
        }

       /* // GET: api/Motivation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MotivationModel>> GetMotivationModel(int id)
        {
          if (_context.Motivations == null)
          {
              return NotFound();
          }
            var motivationModel = await _context.Motivations.FindAsync(id);

            if (motivationModel == null)
            {
                return NotFound();
            }

            return motivationModel;
        }

        // PUT: api/Motivation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotivationModel(int id, MotivationModel motivationModel)
        {
            if (id != motivationModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(motivationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotivationModelExists(id))
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

        // POST: api/Motivation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754*/
        [HttpPost]
        public async Task<ActionResult<MotivationModel>> PostMotivationModel(MotivationModel motivationModel)
        {
          if (_context.Motivations == null)
          {
              return Problem("Entity set 'IntranetDbContext.Motivations'  is null.");
          }
            _context.Motivations.Add(motivationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMotivations), new { id = motivationModel.Id }, motivationModel);
        }

        // DELETE: api/Motivation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotivationModel(int id)
        {
            if (_context.Motivations == null)
            {
                return NotFound();
            }
            var motivationModel = await _context.Motivations.FindAsync(id);
            if (motivationModel == null)
            {
                return NotFound();
            }

            _context.Motivations.Remove(motivationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /*private bool MotivationModelExists(int id)
        {
            return (_context.Motivations?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
