using Microsoft.AspNetCore.Mvc;
using LibraryJulesVerne.Context;
using LibraryJulesVerne.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryJulesVerne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly LibraryJulesVerneContext _context;

        public BooksController(LibraryJulesVerneContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(GetRandomBooks());
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string title)
        {
            return await _context.Books.Where(a => a.Title.Contains(title) || a.Author.Contains(title)).ToListAsync();
        }

        // POST: api/Books/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
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

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
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

        public IActionResult GetRandomBooks()
        {
            var randomBooks = _context.Books
                .OrderBy(b => Guid.NewGuid())
                .Take(10)
                .ToList();

            return View(randomBooks);
        }
    }
}