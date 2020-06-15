using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
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
            var board = await db.Boards.SingleOrDefaultAsync(x => x.Name == name);

            if (board == null)
                return this.Message($"Could not find board {name}", $"Board with the name {name} does not exists");
            return View(board);
        }

        public const int ThreadsChuckCount = 10;

        [Route("{name}/GetThreads")]
        public async Task<JsonResult> GetThreads(string name, int start)
        {
            await using var db = new SquabbyContext();
            var threads = await db.Threads
                .Where(x => x.Board.Name == name)
                .OrderByDescending(t=> t.Rating - (t.CreationDate.Year * 365 + t.CreationDate.Day - (DateTime.Now.Year * 365 + DateTime.Now.Day) * 1000))
                .Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.Content,
                    t.CreationDate,
                    Owner = t.Owner.Username,
                    t.BoardId,
                    Board = name,
                })
                .Skip(start)
                .Take(ThreadsChuckCount)
                .ToArrayAsync();

            if (threads == null) return Json(new {status = "error"});
            else return Json(new {threads});
        }
    }
}