using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Authorization;
using Squabby.Database;
using Squabby.Helpers;
using Squabby.Helpers.Authentication;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.Boards.Thread
{
    [Route("b")]
    [Route("board")]
    public class ThreadController : Controller
    {
        /// <summary>
        /// Show thread
        /// </summary>
        [Route("{name}/{threadId}")]
        public async Task<IActionResult> Overview(string name, int threadId)
        {
            await using var db = new SquabbyContext();
            var thread = await db.Threads
                .Include(x=>x.Owner)
                .Include(x=>x.Comments)
                .SingleOrDefaultAsync(x=> x.Board.Name == name && x.Id == threadId);

            if (thread == null) return this.Message($"Could not find a thread with the id {threadId}",
                $"Thread with the name {threadId} does not exists");
            return View("~/Views/Board/Thread/Overview.cshtml");
        }
        
        /// <summary>
        /// Create new thread
        /// </summary>
        [SquabbyAuthorize]
        //[HttpPost] TODO
        [Route("{name}/PostThread")]
        public async Task<IActionResult> PostThread(string name, Models.Thread thread)
        {
            if (string.IsNullOrWhiteSpace(thread?.Title)
                || thread.Title.Length > Models.Thread.MaxTitleLength
                || thread.Content?.Length > Models.Thread.MaxContentLength) 
                return View("~/Views/Board/CreateBoard.cshtml", new Error(ErrorType.InvalidParameters)); // TODO
            
            await using var db = new SquabbyContext();
            var board = await db.Boards.SingleOrDefaultAsync(x=> x.Name == name);
            if (board == null) return this.Message($"Could not find board {name}", $"Board with the name {name} does not exists");

            var user = HttpContext.GetUser();
            db.Attach(user);
            thread.Owner = user;
            thread.Board = board;
            await db.Threads.AddAsync(thread);
            await db.SaveChangesAsync();
            return this.Message($"Created thread {thread.Title}"); // TODO
        }

        [SquabbyAuthorize]
        //[HttpPost] TODO
        [Route("{name}/{threadId}/Like")]
        public async Task<IActionResult> LikeThread(string name, int threadId)
        {
            Console.WriteLine("Like thread");
            // TODO
            return null;
        }
        
        [SquabbyAuthorize]
        //[HttpPost] TODO
        [Route("{name}/{threadId}/Dislike")]
        public async Task<IActionResult> DislikeThread(string name, int threadId)
        {
            Console.WriteLine("Dislike thread");
            // TODO
            return null;
        }
    }
}