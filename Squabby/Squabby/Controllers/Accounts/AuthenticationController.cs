using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Helpers.Cryptography;

namespace Squabby.Controllers.Accounts
{
    [Route("Account")]
    public class AuthenticationController : Controller
    {
        /// <summary>
        /// Login user with username and password
        /// </summary>
        //[HttpPost] TODO 
        [Route("Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            await using var db = new SquabbyContext();
            var account = db.Accounts.FirstOrDefault(x => x.Username == username);
            if (account == null || !PBKDF2.Verify(account.Password,password))
                return Json(new {status = "error", message = "Wrong username or password"});

            HttpContext.Session.SetAccount(account); 
            return Json(new {status = "success", message = "Successfully logged in"});
        }

        /// <summary>
        /// Logout user
        /// </summary>
        //[HttpPost] TODO
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.LogoutAccount();
            return RedirectToAction("Index", "Home");
        }
    }
}