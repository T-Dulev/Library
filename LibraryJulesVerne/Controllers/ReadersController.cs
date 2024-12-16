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

        // GET: api/Readers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetReaders(string name)
        {
            var res = _context.Readers.Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name)).ToListAsync();
            return await res;
        }

    }
}