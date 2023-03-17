using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntranetPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IntranetDbContext _intranetDbContext;

        public EmployeeController(IntranetDbContext intranetDbContext)
        {
            _intranetDbContext= intranetDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployee()
        {
            if (_intranetDbContext.Employees == null)
            {
                return NotFound();
            }
            return await _intranetDbContext.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployee(int id)
        {
            if (_intranetDbContext.Employees == null)
            {
                return NotFound();
            }
            var employee = await _intranetDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> PostEmployee(EmployeeModel employee)
        {
            _intranetDbContext.Employees.Add(employee);
            await _intranetDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.ID }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id, EmployeeModel employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }

            _intranetDbContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _intranetDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            if (_intranetDbContext.Employees == null)
            {
                return NotFound();
            }
            var employee = await _intranetDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _intranetDbContext.Employees.Remove(employee);
            await _intranetDbContext.SaveChangesAsync();
            return Ok(); 
        }
    }
}
