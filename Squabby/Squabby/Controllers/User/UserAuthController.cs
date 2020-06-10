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
            var user = await db.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null || !PBKDF2.Verify(user.Password, password))
                return View(new Message(MessageType.LoginError));

            HttpContext.SetUser(user);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Login with token 
        /// </summary>
        //[HttpPost]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(string token)
        {
            await using var db = new SquabbyContext();
            var user = await db.Users.SingleOrDefaultAsync(x => x.Token == token);
            if (user == null) return View(new Message(MessageType.LoginError));

            HttpContext.SetUser(user);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Register new user
        /// </summary>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Models.User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                return View("Login", new Message(MessageType.RegisterError));

            await using var db = new SquabbyContext();
            if (await db.Users.AnyAsync(x => x.Username == user.Username))
                return View("Login", new Message(MessageType.RegisterError, "User already exists"));

            user.Role = Role.User;
            user.Password = PBKDF2.Hash(user.Password);
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            HttpContext.SetUser(user);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Register anonymous user
        /// </summary>
        //[HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            var user = new Models.User {Role = Role.Anonymous, Token = Random.GetSecureRandomString(Models.User.TokenLength)};
            await using var db = new SquabbyContext();

            if (await db.Users.AnyAsync(x => x.Token == user.Token)) return await Register();
            await db.Users.AddAsync(user);
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