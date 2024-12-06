using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ReadersController : ControllerBase
{
    private readonly LibraryContext _context;

    public ReadersController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
    {
        return await _context.Readers.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Reader>> CreateReader(Reader reader)
    {
        _context.Readers.Add(reader);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReader), new { id = reader.Id }, reader);
    }

    // GET: api/Readers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Reader>> GetReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);

        if (reader == null)
        {
            return NotFound();
        }

        return reader;
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReader(int id, Reader reader)
    {
        if (id != reader.Id)
        {
            return BadRequest();
        }

        _context.Entry(reader).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReader(int id)
    {
        var reader = await _context.Readers.FindAsync(id);
        if (reader == null)
        {
            return NotFound();
        }

        _context.Readers.Remove(reader);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}