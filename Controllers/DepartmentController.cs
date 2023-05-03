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
    public class DepartmentController : ControllerBase
    {
        private readonly IntranetDbContext _context;

        public DepartmentController(IntranetDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentModel>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentModel>> PostDepartmentModel(DepartmentModel departmentModel)
        {
            _context.Departments.Add(departmentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartments), new { id = departmentModel.ID }, departmentModel);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentModel(int id)
        {
            var departmentModel = await _context.Departments.FindAsync(id);
            if (departmentModel == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(departmentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
