using Microsoft.AspNetCore.Mvc;

namespace Squabby.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index() => View();
        
        [Route("login")]
        public IActionResult Login() => View();
    }
}