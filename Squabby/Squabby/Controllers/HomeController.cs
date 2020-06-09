using Microsoft.AspNetCore.Mvc;

namespace Squabby.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Index")]
        public IActionResult Index() => View();
        
        [Route("Login")]
        public IActionResult Login() => View();
    }
}