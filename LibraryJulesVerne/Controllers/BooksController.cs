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

        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedBookDto>> GetBookDetails(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookLoans)
                .ThenInclude(bl => bl.Reader)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var lastFiveLoans = book.BookLoans
                .OrderByDescending(l => l.borrowed_date)
                .Take(5)
                .Select(l => new BookLoanDto
                {
                    LoanId = l.loan_id,
                    BorrowedDate = l.borrowed_date,
                    ReturnedDate = l.returned_date,
                    BorrowerName = l.Reader?.FirstName + ' ' + l.Reader?.LastName
                })
                .ToList();

            var bookDetails = new DetailedBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Isbn = book.Isbn,
                Author = book.Author,
                Genre = book.Genre,
                AvailableCount = book.AvailableCount - _context.BookLoans.Count(l => l.book_id == book.Id && l.returned_date == null),
                LastFiveLoans = lastFiveLoans
            };

            return Ok(bookDetails);
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