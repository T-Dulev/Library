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

    }
}