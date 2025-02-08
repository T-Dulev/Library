using LibraryJulesVerne.Context;
using LibraryJulesVerne.Models;
using LibraryJulesVerne.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Diagnostics;

namespace LibraryJulesVerne.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(GetRandomBooks());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Books()
        {
            return View();
        }

        public IActionResult BookCreate()
        {
            return View();
        }

        public IActionResult Readers()
        {
            return View();
        }

        // GET: Home/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _context = new LibraryJulesVerneContext();
            var reader = await _context.Readers
                .Include(r => r.BookLoans)
                .ThenInclude(bl => bl.Book)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reader == null)
            {
                return NotFound();
            }

            // Сортираме книгите според условията
            reader.BookLoans = reader.BookLoans
                .OrderByDescending(bl => !bl.returned_date.HasValue) // Невърнатите книги първо
                .ThenByDescending(bl => bl.borrowed_date) // В обратен ред на датата на взимане
                .ToList();

            return View(reader);
        }

        // POST: Home/ReturnBook/{loanId}
        public async Task<IActionResult> ReturnBook(int loanId)
        {
            var _context = new LibraryJulesVerneContext();
            var bookLoan = await _context.BookLoans
                .Include(bl => bl.Book)
                .Include(bl => bl.Reader)
                .FirstOrDefaultAsync(bl => bl.loan_id == loanId);

            if (bookLoan == null || bookLoan.returned_date != null)
            {
                return BadRequest();
            }

            bookLoan.returned_date = DateTime.Now;
            await _context.SaveChangesAsync();

            // Запазване на съобщение за успех в TempData
            TempData["SuccessMessage"] = "Книгата беше успешно върната.";

            string refererUrl = Request.Headers["Referer"].ToString();

            if (refererUrl.Contains("/Details"))
            {
                // Пренасочва към страницата с детайли на читателя
                return RedirectToAction("Details", new { id = bookLoan.Reader.Id });
            }
            else if (refererUrl.Contains("/BookTaken"))
            {
                // Пренасочва към страницата със списъка с взети книги
                return RedirectToAction("BookTaken");
            }
            else
            {
                // По подразбиране пренасочва към началната страница или друга подходяща страница
                return RedirectToAction("Index");
            }
        }

        // POST: Home/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            var _context = new LibraryJulesVerneContext();
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(BooksController.GetBook), new { id = book.Id }, book);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<Book> GetRandomBooks()
        {
            var dbContext = new LibraryJulesVerneContext();
            
            var books = dbContext.Books.ToList();

            var random = new Random();
            var randomBooks = new List<Book>();
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = random.Next(dbContext.Books.Count() - 1);
                randomBooks.Add(books[randomIndex]);
            }
            //var books = dbContext.Books
            //    .OrderBy(b => b.Id)
            //    .Skip(randomIndex)
            //    .Take(3)
            //    .ToList();

            return randomBooks;
        }

        public IActionResult BookTaken()
        {
            var unreturnedBooks = GetUnreturnedBooks();

            return View(unreturnedBooks);
        }

        public List<UnreturnedBookDto> GetUnreturnedBooks()
        {
            using var _context = new LibraryJulesVerneContext();
            var unreturnedBooks = _context.BookLoans
                .Include(bl => bl.Book)
                .Include(bl => bl.Reader)
                .Where(bl => bl.returned_date == null)
                .Select(bl => new UnreturnedBookDto
                {
                    loan_id = bl.loan_id,   
                    Title = bl.Book.Title,
                    Author = bl.Book.Author,
                    ReaderId = bl.reader_id,
                    FirstName = bl.Reader.FirstName,
                    LastName = bl.Reader.LastName,
                    EGN = bl.Reader.EGN,
                    Email = bl.Reader.Email,
                    BorrowedDate = DateOnly.FromDateTime(bl.borrowed_date)
                })
                .ToList();

            return unreturnedBooks;
        }

        [HttpPost("TakeBook")]
        public async Task<IActionResult> TakeBook([FromQuery] int bookId, [FromQuery] int readerId)
        {
            using var _context = new LibraryJulesVerneContext();
            var book = await _context.Books.FindAsync(bookId);
            var reader = await _context.Readers.FindAsync(readerId);

            if (book == null || reader == null)
            {
                return BadRequest("Невалидни идентификатори.");
            }

            if (book.AvailableCount <= 0)
            {
                return BadRequest("Книгата не е налична.");
            }

            var bookLoan = new BookLoan
            {
                Book = book,
                Reader = reader,
                borrowed_date = DateTime.Now
            };

            _context.Add(bookLoan);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

