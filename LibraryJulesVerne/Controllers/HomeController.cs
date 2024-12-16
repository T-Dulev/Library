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

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult BookCreate()
        {
            return View();
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

    }
}
