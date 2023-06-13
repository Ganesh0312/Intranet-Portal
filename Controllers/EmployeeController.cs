using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntranetPortal.Models;
using Intranet_Portal.Models;

namespace IntranetPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IntranetDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeeController(IntranetDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
        {
            return await _context.EmployeesModel
                .Select(x => new EmployeeModel()
                {
                    employeesID = x.employeesID,
                    employeeName = x.employeeName,
                    imageName = x.imageName,
                    mail = x.mail,
                    mobile = x.mobile,
                    dob = x.dob,
                    password = x.password,
                    dateOfJoin = x.dateOfJoin,
                    department = x.department,
                    IsActive =x.IsActive,
                    designation = x.designation,
                    imageSrc = String.Format("{0}://{1}{2}/EmpImage/{3}", Request.Scheme, Request.Host, Request.PathBase, x.imageName)
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeModel(int id)
        {
            var employeeModel = await _context.EmployeesModel.FindAsync(id);

            if (employeeModel == null)
            {
                return NotFound(new { Message = "Employee not Found" });
            }

            return employeeModel;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeModel(int id, [FromForm] EmployeeModel employeeModel)
        {
            if (id != employeeModel.employeesID)
            {
                return BadRequest(new { Message = "Put id Or EmoployeeModel Id Not match" });
            }

            if (employeeModel.imageFile != null)
            {
                DeleteImage(employeeModel.imageName);
                employeeModel.imageName = await SaveImage(employeeModel.imageFile);
            }

            _context.Entry(employeeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(id))
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

        
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> PostEmployeeModel([FromForm] EmployeeModel employeeModel)
        {
            if (await _context.EmployeesModel.AnyAsync(e => e.mail == employeeModel.mail))
            {
                return Ok(new { Message = "Employee Allready Exist" });
            }

            employeeModel.imageName = await SaveImage(employeeModel.imageFile);
            _context.EmployeesModel.Add(employeeModel);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Employee Added Successfully" });
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeModel>> DeleteEmployeeModel(int id)
        {
            var employeeModel = await _context.EmployeesModel.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            DeleteImage(employeeModel.imageName);
            _context.EmployeesModel.Remove(employeeModel);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Employee Deleted Successfully" });
        }
       
       [HttpGet("birthday")]
        public ActionResult<List<EmployeeModel>> EmployeeBirthday()
        {
            var employees = _context.EmployeesModel.ToList();
            var today = DateTime.Today;

            var employeesWithBirthdayToday = employees.Where(e => e.dob.Month == today.Month && e.dob.Day == today.Day).ToList();

            return employeesWithBirthdayToday;
        }
        [HttpGet("NewJoiner")]
        public ActionResult<List<EmployeeModel>> GetNewJoiners()
        {
            var employees = _context.EmployeesModel.OrderByDescending(e => e.dateOfJoin).ToList();
            return employees;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(Login employee)
        {
            var existingEmployee = await _context.EmployeesModel.FirstOrDefaultAsync(e => e.mail == employee.Email && e.password == employee.Password);

            if (existingEmployee == null)
            {
                return BadRequest(new { message = "Invalid email or password" });
            }
            else if (existingEmployee.IsActive == false)
            {
                return BadRequest(new { message = "Your account is inactive. Please contact your administrator" });
            }
            else
            {
                return Ok(new { message = "Employee logged in successfully" });
            }
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.EmployeesModel.Any(e => e.employeesID == id);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "EmpImage", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "EmpImage", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
