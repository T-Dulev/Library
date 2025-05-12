using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryJulesVerne.Controllers
{
    [Authorize]
    public class ProtectedController : ControllerBase
    {
        // Този метод ще бъде достъпен само ако потребителят е автентикиран
        [HttpGet]
        public IActionResult GetProtectedData()
        {
            return Ok("Това е защитена информация.");
        }
    }
}