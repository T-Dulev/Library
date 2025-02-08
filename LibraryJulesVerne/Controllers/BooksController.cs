using Microsoft.AspNetCore.Mvc;
using LibraryJulesVerne.Context;
using LibraryJulesVerne.Models;
using Microsoft.EntityFrameworkCore;
using LibraryJulesVerne.DTOs;

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
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string title)
        {
            var booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title) && title != "*")
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(title) || b.Author.Contains(title));
            }

            var books = await booksQuery.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                Author = b.Author,
                Genre = b.Genre,
                AvailableCount = b.AvailableCount - _context.BookLoans.Count(l => l.book_id == b.Id && l.returned_date == null)
            }).ToListAsync();

            return Ok(books);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }

            // Първо изтриваме всички BookLoans, свързани с тази книга
            var loansToDelete = _context.BookLoans.Where(bl => bl.book_id == id).ToList();
            _context.BookLoans.RemoveRange(loansToDelete);

            // След това изтриваме самата книга
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //// GET: api/Books/5
        //[HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        //// PUT: api/Books/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //// POST: api/Books
        //[HttpPost]
        //public async Task<ActionResult<Book>> PostBook(Book book)
        //{
        //    _context.Books.Add(book);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        //}

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