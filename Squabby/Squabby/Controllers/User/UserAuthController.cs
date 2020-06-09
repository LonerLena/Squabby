using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Helpers.Cryptography;
using Squabby.Models;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.User
{
    public class UserController : Controller
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

            HttpContext.SetUser(account); 
            return RedirectToAction("Index", "Home"); 
        }

        /// <summary>
        /// Register new user user
        /// </summary>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Models.User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                return View("Login", new Message(MessageType.RegisterError));

            await using var db = new SquabbyContext();
            if (await db.Accounts.AnyAsync(x => x.Username == user.Username))
                return View("Login", new Message(MessageType.RegisterError, "User already exists"));

            user.Role = Role.User;
            user.Password = PBKDF2.Hash(user.Password);
            await db.Accounts.AddAsync(user);
            await db.SaveChangesAsync();
            
            HttpContext.SetUser(user);
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// Logout user
        /// </summary>
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.LogoutUser();
            return RedirectToAction("Index", "Home");
        }
    }
}