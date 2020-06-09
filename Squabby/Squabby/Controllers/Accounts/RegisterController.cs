using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Helpers.Cryptography;
using Squabby.Models;

namespace Squabby.Controllers.Accounts
{
    [Route("Account")]
    public class RegisterController : Controller
    {
        /// <summary>
        /// Register new user account
        /// </summary>
        [Route("Register")]
        public async Task<IActionResult> Register(Account account)
        {
            if (string.IsNullOrEmpty(account.Username) || string.IsNullOrEmpty(account.Password))
                return Json(new {status = "error", message = "Username or password is empty"});

            await using var db = new SquabbyContext();
            if (db.Accounts.Any(x => x.Username == account.Username))
                return Json(new {status = "error", message = "Account already exists"});

            account.Role = Role.User;
            account.Password = PBKDF2.Hash(account.Password);
            db.Accounts.Add(account);
            await db.SaveChangesAsync();
            
            HttpContext.Session.SetAccount(account);
            return Json(new {status = "success", message = "Account successfully created"});
        }
    }
}