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
        
        [Route("Register")]
        public IActionResult Register() => View();
        
        [Route("TokenLogin")]
        public IActionResult TokenLogin() => View();

        /// <summary>
        /// Login user with username and password
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return View(new Error(ErrorType.LoginError));
            await using var db = new SquabbyContext();
            var user = await db.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null || !PBKDF2.Verify(user.Password, password))
                return View(new Error(ErrorType.LoginError));

            HttpContext.SetUser(user);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Login with token 
        /// </summary>
        [HttpPost]
        [Route("TokenLogin")]
        public async Task<IActionResult> TokenLogin(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return View(new Error(ErrorType.LoginError));
            await using var db = new SquabbyContext();
            var user = await db.Users.SingleOrDefaultAsync(x => x.Token == token);
            if (user == null) return View(new Error(ErrorType.LoginError));

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
                return View(new Error(ErrorType.Unknown));

            await using var db = new SquabbyContext();
            if (await db.Users.AnyAsync(x => x.Username == user.Username))
                return View(new Error(ErrorType.NameAlreadyUsedError, "User already exists"));

            user.UserRole = UserRole.User;
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
        [Route("TokenRegister")]
        public async Task<IActionResult> TokenRegister()
        {
            var user = new Models.User {UserRole = UserRole.Anonymous, Token = Random.GetSecureRandomString(Models.User.TokenLength)};
            await using var db = new SquabbyContext();

            if (await db.Users.AnyAsync(x => x.Token == user.Token)) return await TokenRegister();
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