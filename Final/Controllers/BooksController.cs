using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final.Models;  

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int?}")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks(int? id)
    {
        if (id == null || id == 0)
        {
            return await _context.Books.Take(5).ToListAsync();
        }
        else
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return new List<Book> { book };
        }
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetBooks", new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

