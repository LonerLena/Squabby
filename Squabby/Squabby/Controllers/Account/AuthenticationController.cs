using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Helpers.Cryptography;
using Squabby.Models;

namespace Squabby.Controllers.Account
{
    public class AuthenticationController : Controller
    {
        [Route("Login")]
        public IActionResult Login() => View();
        
        /// <summary>
        /// Login user with username and password
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            await using var db = new SquabbyContext();
            var account = db.Accounts.FirstOrDefault(x => x.Username == username);
            if (account == null || !PBKDF2.Verify(account.Password, password))
                return View(new Message(MessageType.LoginError));

            HttpContext.Session.SetAccount(account); 
            return RedirectToAction("Index", "Home"); 
        }

        /// <summary>
        /// Register new user account
        /// </summary>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Models.Account account)
        {
            if (string.IsNullOrEmpty(account.Username) || string.IsNullOrEmpty(account.Password))
                return View("Login", new Message(MessageType.RegisterError));

            await using var db = new SquabbyContext();
            if (db.Accounts.Any(x => x.Username == account.Username))
                return View("Login", new Message(MessageType.RegisterError, "User already exists"));

            account.Role = Role.User;
            account.Password = PBKDF2.Hash(account.Password);
            db.Accounts.Add(account);
            await db.SaveChangesAsync();
            
            HttpContext.Session.SetAccount(account);
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// Logout user
        /// </summary>
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.LogoutAccount();
            return RedirectToAction("Index", "Home");
        }
    }
}