using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;

namespace Squabby.Controllers.Boards
{
    [Route("/api/b")]
    [Route("/api/board")]
    public class BoardApi : Controller
    {
        public const int ThreadsChuckSize = 50;

        [Route("GetThreads")]
        public async Task<JsonResult> GetThreads(short boardId, int chunk)
        {
            await using var db = new SquabbyContext();
            var threads = await db.Threads
                .Where(x => x.BoardId == boardId)
                .OrderByDescending(t => t.Rating)
                .Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.Content,
                    t.CreationDate,
                    Owner = t.Owner.Username,
                    t.BoardId,
                    Board = t.Board.Name
                })
                .Skip(chunk * ThreadsChuckSize)
                .Take(ThreadsChuckSize)
                .ToArrayAsync();

            if (threads == null) return Json(new {status = "error"});
            else return Json(new {status = "success", threads});
        }
    }
}