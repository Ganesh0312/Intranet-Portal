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
    public class DesignationController : ControllerBase
    {
        private readonly IntranetDbContext _context;

        public DesignationController(IntranetDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DesignationModel>>> GetDesignations()
        {
            return await _context.Designations.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DesignationModel>> PostDesignationModel(DesignationModel designationModel)
        {
            _context.Designations.Add(designationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDesignations), new { id = designationModel.ID }, designationModel);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesignationModel(int id)
        {
            var designationModel = await _context.Designations.FindAsync(id);
            if (designationModel == null)
            {
                return NotFound();
            }

            _context.Designations.Remove(designationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       /* private bool DesignationModelExists(int id)
        {
            return _context.Designations.Any(e => e.ID == id);
        }*/
    }
}
 