using Intranet_Portal.Models;
using IntranetPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        public readonly IntranetDbContext _context;

        public PollController(IntranetDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPoll()
        {
            return await _context.polls.ToListAsync();
        }
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Poll>>> GetDepartments()
        {
            return await _context..ToListAsync();
        }*/

        [HttpPost]
        public async Task<ActionResult<Poll>> PostPoll([FromForm] Poll poll)
        {
            _context.polls.Add(poll);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{pollId}/Vote/{option}")]
        public async Task<IActionResult> VoteForOption(int pollId, int option)
        {
            var poll = await _context.polls.FindAsync(pollId);
            if (poll == null)
            {
                return NotFound("Poll not found");
            }

            if (option < 1 || option > 4)
            {
                return BadRequest("Invalid option");
            }

            _context.Votes.Add(new Vote { PollId = pollId, OptionNumber = option });
            await _context.SaveChangesAsync();

            return Ok("Vote submitted successfully");
        }

        [HttpGet("{pollId}/VoteCount")]
        public async Task<IActionResult> GetVoteCount(int pollId)
        {
            var poll = await _context.polls.FindAsync(pollId);
            if (poll == null)
            {
                return NotFound("Poll not found");
            }

            var voteCounts = new Dictionary<int, int>
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 }
        };

            foreach (var vote in _context.Votes)
            {
                voteCounts[vote.OptionNumber]++;
            }

            return Ok(voteCounts);
        }
    }
}
