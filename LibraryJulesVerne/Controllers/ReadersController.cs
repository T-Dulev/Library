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


        [HttpGet("search")]
        public async Task<IActionResult> SearchReaders([FromQuery] SearchModel searchModel)
        {
            var readers = await _context.Readers
                .Where(r => r.FirstName.Contains(searchModel.Name) || r.LastName.Contains(searchModel.Name))
                .ToListAsync();

            return Ok(readers);
        }
    }
}