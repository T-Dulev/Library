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
            var booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title) && title != "*")
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(title) || b.Author.Contains(title));
            }

            var joinedQuery = from book in booksQuery
                              join loan in _context.BookLoans on book.Id equals loan.book_id into loansGrouped
                              from loan in loansGrouped.DefaultIfEmpty()
                              select new
                              {
                                  Book = book,
                                  AvailableCount = book.AvailableCount - loansGrouped.Count(l => l.returned_date == null)
                              };

            var result = await joinedQuery.Select(j => j.Book)
                .Include(bl => bl.BookLoans)
                .ToListAsync();

            return Ok(result);
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
        public IActionResult DeleteBook(int id)
        {
            var bookToDelete = _context.Books.FirstOrDefault(b => b.Id == id);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
                return NoContent(); // Връща статус код 204 No Content
            }
            else
            {
                return NotFound(); // Връща статус код 404 Not Found, ако книгата не е намерена
            }
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