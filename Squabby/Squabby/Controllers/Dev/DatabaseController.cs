using System.Linq;
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

            db.Users.Add(new Models.User {Username = "Admin", Password = PBKDF2.Hash("Admin"), UserRole = UserRole.Admin});
            db.Users.Add(new Models.User {Username = "User", Password = PBKDF2.Hash("User"), UserRole = UserRole.User});
            await db.SaveChangesAsync();
            return "New database is created";
        }
        
        [Route("CreateThreads")]
        public async Task<string> CreateThreads(string board, int amount)
        {
            await using var db = new SquabbyContext();

            var b = db.Boards.First(x => x.Name == board);
            for (int i = 0; i < amount; i++)
                db.Threads.Add(new Thread{Title = $"thread title {i}", Content = "thread content", Board = b});

            await db.SaveChangesAsync();
            return "threads are created";
        }
    }
#endif
}