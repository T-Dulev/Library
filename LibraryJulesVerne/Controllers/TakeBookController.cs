using Microsoft.AspNetCore.Mvc;
using LibraryJulesVerne.Models;
using LibraryJulesVerne.Context;

namespace LibraryJulesVerne.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TakeBookController : ControllerBase
    {
        private readonly LibraryJulesVerneContext _context;

        public TakeBookController(LibraryJulesVerneContext context)
        {
            _context = context;
        }

        [HttpPost("TakeBook")]
        public async Task<IActionResult> TakeBook([FromQuery] int bookId, [FromQuery] int readerId)
        {
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