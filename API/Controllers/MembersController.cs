using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers
{
    [Route("api/[controller]")]// localhost:5001/api/members
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return members;            
        }

        
        [HttpGet("{id}")]// localhost:5001/api/members/bob-id
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id);
            if (member == null) return NotFound();
            return member;            
        }

        // POST: api/Members
        [HttpPost]
        public IActionResult CreateMember([FromBody] string member)
        {
            // Logic to create a new member would go here
            return CreatedAtAction(nameof(GetMember), new { id = 1 }, member);
        }

        // PUT: api/Members/5
        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, [FromBody] string member)
        {
            // Logic to update an existing member would go here
            return NoContent();
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            // Logic to delete a member would go here
            return NoContent();
        }
    }
}