using Intranet_Portal.Models;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatrixController : ControllerBase
    {
        private readonly IntranetDbContext _context;
        public MatrixController(IntranetDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddMatrix([FromForm] EscalationMatrix matrix)
        {
            // Assuming _context is the database context and Employee model is available

            // Retrieve the employee with the provided employeeId
            var employee = await _context.EmployeesModel.FindAsync(matrix.EmployeeId);

            if (employee == null)
            {
                return BadRequest(new { Message = "Invalid Employee OR Employee Not found " });
            }

            // Check if the employeeId in the matrix matches the employeeId in the employee model
            if (matrix.EmployeeId != employee.employeesID)
            {
                return BadRequest(new { Message = "Mismatch between EmployeeId in matrix and EmployeeId in employee model" });
            }

            _context.EMatrix.Add(matrix);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Escalation Matrix Added Successfully" });
        }


        /*
                [HttpPost]
                public async Task<ActionResult> AddMatrix([FromForm] EscalationMatrix matrix)
                {
                    _context.EMatrix.Add(matrix);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Escalation Matrix Added Successfully" });
                }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscalationMatrix>>> GetMatrix()
        {
            return await _context.EMatrix.ToListAsync();
        }
    }
}
