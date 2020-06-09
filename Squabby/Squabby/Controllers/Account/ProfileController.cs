using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;

namespace Squabby.Controllers.Account
{
    [Route("u")]
    [Route("user")]
    public class ProfileController : Controller
    {
        [Route("{username}")]
        public async Task<ViewResult> Profile(string username)
        {
            await using var db = new SquabbyContext();
            var user = await db.Accounts.FirstOrDefaultAsync(x => x.Username == username);
            //if (user == null) return error; TODO
            return View(user);
        }
    }
}