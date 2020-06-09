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

            var adminAccount = new Models.Account {Username = "Admin", Password = PBKDF2.Hash("Admin"), Role = Role.Admin};
            db.Accounts.Add(adminAccount);
            await db.SaveChangesAsync();
            return "success";
        }
    }
#endif
}