using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.User
{
    [Route("u")]
    [Route("user")]
    public class ProfileController : Controller
    {
        /// <summary>
        /// Show specific user profile
        /// </summary>
        [Route("{username}")]
        public async Task<ViewResult> Profile(string username)
        {
            await using var db = new SquabbyContext();
            var user = await db.Accounts.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Could not find user {username}", $"User with the name {username} does not exists")); 
            return View("~/Views/User/Profile.cshtml",user);
        }
    }
}