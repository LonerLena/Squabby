using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;
using Squabby.Helpers;
using Squabby.Helpers.Authentication;
using Squabby.Models;

namespace Squabby.Controllers.Boards
{
    public class BoardSettingsController : Controller
    {
        [Route("CreateBoard")]
        public async Task<ViewResult> CreateBoard(string name)
        {
            await using var db = new SquabbyContext();
            if (await db.Boards.AnyAsync(x => x.Name == name))
                return this.Message($"Could not create board {name}", $"Board with the name {name} already exists");

            var user = await db.Users.SingleOrDefaultAsync(x => x.Username == HttpContext.GetUser().Username);
            await db.AddAsync(new Board {Name = name, Owner = user });
            await db.SaveChangesAsync();
            return this.Message($"Created board {name}");
        } 
    }
}