using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final.Models;  

[Route("api/[controller]")]
[ApiController]
public class HobbiesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HobbiesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int?}")]
    public async Task<ActionResult<IEnumerable<Hobby>>> GetHobbies(int? id)
    {
        if (id == null || id == 0)
        {
            return await _context.Hobbies.Take(5).ToListAsync();
        }
        else
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return NotFound();
            }
            return new List<Hobby> { hobby };
        }
    }

    [HttpPost]
    public async Task<ActionResult<Hobby>> PostHobby(Hobby hobby)
    {
        _context.Hobbies.Add(hobby);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetHobbies", new { id = hobby.Id }, hobby);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutHobby(int id, Hobby hobby)
    {
        if (id != hobby.Id)
        {
            return BadRequest();
        }

        _context.Entry(hobby).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHobby(int id)
    {
        var hobby = await _context.Hobbies.FindAsync(id);
        if (hobby == null)
        {
            return NotFound();
        }

        _context.Hobbies.Remove(hobby);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

