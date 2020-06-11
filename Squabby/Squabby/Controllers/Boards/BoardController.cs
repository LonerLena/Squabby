using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;
using Squabby.Helpers;

namespace Squabby.Controllers.Boards
{
    [Route("b")]
    [Route("board")]
    public class BoardController : Controller
    {
        /// <summary>
        /// Show board
        /// </summary>
        [Route("{name}")]
        public async Task<IActionResult> Overview(string name)
        {
            await using var db = new SquabbyContext();
            var board = await db.Boards
                .Include(x=>x.Threads)
                .ThenInclude(x=>x.Owner)
                .SingleOrDefaultAsync(x => x.Name == name);
            
            if (board == null) return this.Message($"Could not find board {name}", $"Board with the name {name} does not exists");
            return View(board);
        }
        
        [Route("{name}/GetThreads")]
        public async Task<JsonResult> GetThreads(string name, int index, int amount = 10)
        {
            await using var db = new SquabbyContext();
            var threads = await db.Threads
                .Include(x=>x)
                .Where(x=> x.Board.Name == name)
                .Select(t=>new 
                {
                    t.Title,
                    t.Content,
                    t.CreationDate,
                    t.Owner.Username
                })
                .Skip(index)
                .Take(amount)
                .ToArrayAsync();

            if (threads == null) return Json(new {status = "error"});
            else return Json(new { threads  });
        }
    }
}