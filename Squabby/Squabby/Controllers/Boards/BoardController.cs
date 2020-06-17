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
            var board = await db.Boards.FirstOrDefaultAsync(x => x.Name == name);

            if (board == null) return this.Message($"Could not find board {name}", $"Board with the name {name} does not exists");
            return View(board);
        }
    }
}