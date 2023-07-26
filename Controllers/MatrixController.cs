using Intranet_Portal.Models;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatrixController : ControllerBase
    {
        readonly IntranetDbContext _dbContext;
        public MatrixController(IntranetDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EscalationMatrix>>> GetEscalationTopics()
        {
            return await _dbContext.Escalations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EscalationMatrix>> GetEscalationTopic(int id)
        {
            if (_dbContext.Escalations == null)
            {
                return NotFound();
            }
            var motivationModel = await _dbContext.Escalations.FindAsync(id);

            if (motivationModel == null)
            {
                return NotFound();
            }
            return motivationModel;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEscalationTopic(EscalationMatrix escalationTopic)
        {
            _dbContext.Escalations.Add(escalationTopic);
            await _dbContext.SaveChangesAsync();
            return Ok(new {Message="EsclationMatrix Added Successfully"});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEscalationTopic(int id, EscalationMatrix updatedEscalationTopic)
        {
            var escalationTopic = await _dbContext.Escalations
                .Include(et => et.ResponsibleEmployees)
                .FirstOrDefaultAsync(et => et.Id == id);

            if (escalationTopic == null)
            {
                return NotFound();
            }

            escalationTopic.TopicName = updatedEscalationTopic.TopicName;
            escalationTopic.ResponsibleEmployees = updatedEscalationTopic.ResponsibleEmployees;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEscalationTopic(int id)
        {
            var escalationTopic = await _dbContext.Escalations
                .FirstOrDefaultAsync(et => et.Id == id);

            if (escalationTopic == null)
            { 
                return NotFound();
            }

            _dbContext.Escalations.Remove(escalationTopic);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
}
}
