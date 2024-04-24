using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final.Models;  

[Route("api/[controller]")]
[ApiController]
public class FavoriteBreakfastFoodsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FavoriteBreakfastFoodsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int?}")]
    public async Task<ActionResult<IEnumerable<FavoriteBreakfastFood>>> GetFavoriteBreakfastFoods(int? id)
    {
        if (id == null || id == 0)
        {
            return await _context.FavoriteBreakfastFoods.Take(5).ToListAsync();
        }
        else
        {
            var food = await _context.FavoriteBreakfastFoods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return new List<FavoriteBreakfastFood> { food };
        }
    }

    [HttpPost]
    public async Task<ActionResult<FavoriteBreakfastFood>> PostFavoriteBreakfastFood(FavoriteBreakfastFood food)
    {
        _context.FavoriteBreakfastFoods.Add(food);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetFavoriteBreakfastFoods", new { id = food.Id }, food);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFavoriteBreakfastFood(int id, FavoriteBreakfastFood food)
    {
        if (id != food.Id)
        {
            return BadRequest();
        }

        _context.Entry(food).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFavoriteBreakfastFood(int id)
    {
        var food = await _context.FavoriteBreakfastFoods.FindAsync(id);
        if (food == null)
        {
            return NotFound();
        }

        _context.FavoriteBreakfastFoods.Remove(food);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

