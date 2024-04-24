using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final.Models;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet("{id:int?}")]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeamMembers(int? id)
        {
            if (id == null || id == 0)
            {
               
                return await _context.TeamMembers.Take(5).ToListAsync();
            }
            else
            {
                var teamMember = await _context.TeamMembers.FindAsync(id);
                if (teamMember == null)
                {
                    return NotFound();
                }
                return new List<TeamMember> { teamMember };
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
        {
            if (id != teamMember.Id)
            {
                return BadRequest();
            }

            _context.Entry(teamMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(id))
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
        public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
        {
            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamMembers", new { id = teamMember.Id }, teamMember);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamMemberExists(int id)
        {
            return _context.TeamMembers.Any(e => e.Id == id);
        }
    }
}

