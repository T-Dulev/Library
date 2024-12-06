using LibraryJulesVerne.Context;
using LibraryJulesVerne.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryJulesVerne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorsController : Controller
    {

        public IActionResult Index()
        {
            var dbContext = new LibraryJulesVerneContext();
            var books = dbContext.Books.ToList();
            return View(books);
        }

        [HttpPost]
        public IActionResult GetTextLength([FromBody] Models.TextLengthModel model)
        {
            var dbContext = new LibraryJulesVerneContext();
            var books = dbContext.Books.Where(a => a.Id == 1).ToList();
            return View(books);
        }

        public class TextLengthController : ControllerBase
        {
            [HttpPost]
            public IActionResult GetTextLength([FromBody] TextLengthModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var textLength = model.InputText?.Length ?? 0;
                return Ok(new { Length = textLength });
            }
        }
    }


}
