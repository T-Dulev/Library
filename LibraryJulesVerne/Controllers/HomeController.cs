using LibraryJulesVerne.Context;
using LibraryJulesVerne.Models;
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
            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
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

        public List<UnreturnedBook> GetUnreturnedBooks()
        {
            using var _context = new LibraryJulesVerneContext();
            var unreturnedBooks = _context.BookLoans
                .Include(bl => bl.Book)
                .Include(bl => bl.Reader)
                .Where(bl => bl.returned_date == null)
                .Select(bl => new UnreturnedBook
                {
                    Title = bl.Book.Title,
                    Author = bl.Book.Author,
                    FirstName = bl.Reader.FirstName,
                    LastName = bl.Reader.LastName,
                    EGN = bl.Reader.EGN,
                    Email = bl.Reader.Email,
                    BorrowedDate = DateOnly.FromDateTime(bl.borrowed_date)
                })
                .ToList();

            return unreturnedBooks;
        }

    }
}

