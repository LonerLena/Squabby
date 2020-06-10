using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Models;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.Boards
{
    [Route("b")]
    [Route("board")]
    public class BoardController : Controller
    {
        [Route("{name}")]
        public async Task<ViewResult> Overview(string name)
        {
            await using var db = new SquabbyContext();
            var board = await db.Boards
                .Include(x=>x.Owner)
                .Include(x=>x.Threads)
                .SingleOrDefaultAsync(x => x.Name == name);
            if (board == null) 
                return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Could not find board {name}", $"Board with the name {name} does not exists"));
            return View(board);
        }
        
        [Route("{name}/Thread")]
        public async Task<ViewResult> Thread(string name, int id)
        {
            await using var db = new SquabbyContext();
            var thread = await db.Threads
                .Include(x=>x.Owner)
                .SingleOrDefaultAsync(x=>x.Id == id);
            
            if(thread == null) return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Could not find a thread with the id {id}", $"Thread with the name {id} does not exists"));
            await db.SaveChangesAsync();
            return View(thread);
        }
        
        [Route("{name}/CreateThread")]
        public async Task<ViewResult> CreateThread(string name, Thread thread)
        {
            await using var db = new SquabbyContext();
            var board = await db.Boards.SingleOrDefaultAsync(x=>x.Name == name);
            if(board == null)
                return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Could not find board {name}", $"Board with the name {name} does not exists"));
            thread.Board = board;
            await db.Threads.AddAsync(thread);
            await db.SaveChangesAsync();
            return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Created thread {thread.Title}"));
        }
    }
}