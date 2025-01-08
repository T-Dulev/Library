using Microsoft.AspNetCore.Mvc;
using LibraryJulesVerne.Context;
using LibraryJulesVerne.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryJulesVerne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadersController : Controller
    {
        private readonly LibraryJulesVerneContext _context;

        public ReadersController(LibraryJulesVerneContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Readers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetReaders(string name)
        {
            if (name != "*")
            {
                return await _context.Readers.Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name)).ToListAsync();
            }
            else 
            {
                return await _context.Readers.ToListAsync();
            }
        }

        // GET: Readers/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: /Readers/AddBookToReader
        [HttpPost]
        public IActionResult AddBookToReader(int readerId, int bookId)
        {
            var reader = _context.Readers.Find(readerId);
            var book = _context.Books.Find(bookId);

            if (reader != null && book != null)
            {
                var newLoan = new BookLoan
                {
                    book_id = book.Id,
                    reader_id = reader.Id,
                    borrowed_date = DateTime.Now,
                    returned_date = null
                };

                _context.BookLoans.Add(newLoan);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", new { id = readerId });
        }
    }
}