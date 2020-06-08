using Microsoft.AspNetCore.Mvc;

namespace Squabby.Controllers
{
    public class HomeController : Controller
    {
        [Route("Login")]
        public IActionResult Login()
            => View();
    }
}