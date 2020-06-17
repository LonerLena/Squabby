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
        [Route("InitDatabase")]
        public async Task<string> InitDatabase()
        {
            await using var db = new SquabbyContext();
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();
            await db.SaveChangesAsync();
            return "Empty database is created";
        }
        
        [Route("CreateDatabase")]
        public async Task<string> CreateDatabase()
        {
            await using var db = new SquabbyContext();
            await InitDatabase();

            var user = new Models.User {Username = "User", Password = PBKDF2.Hash("User"), UserRole = UserRole.User};
            await db.Users.AddAsync(user);
            await db.Users.AddAsync(new Models.User {Username = "Admin", Password = PBKDF2.Hash("Admin"), UserRole = UserRole.Admin});

            var board = new Board {Name = "Board", Description = "Board description", Owner = user};
            await db.Boards.AddAsync(board);
            for (int i = 0; i < 1_000; i++) await db.Threads.AddAsync(new Thread{Title = $"Thread {i}", Content = "Thread content", Board = board, Rating = i});
            
            await db.SaveChangesAsync();
            return "New database is created";
        }
    }
#endif
}