using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squabby.Database;
using Squabby.Helpers.Cryptography;
using Squabby.Models;

namespace Squabby.Controllers.Dev
{
#if DEBUG
    [Route("dev")]
    public class DevelopController : Controller
    {
        [Route("CreateDatabase")]
        public async Task<string> CreateDatabase()
        {
            await using var db = new SquabbyContext();
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            db.Users.Add(new Models.User {Username = "Admin", Password = PBKDF2.Hash("Admin"), Role = Role.Admin});
            db.Users.Add(new Models.User {Username = "User", Password = PBKDF2.Hash("User"), Role = Role.Admin});
            await db.SaveChangesAsync();
            return "New database is created";
        }
    }
#endif
}