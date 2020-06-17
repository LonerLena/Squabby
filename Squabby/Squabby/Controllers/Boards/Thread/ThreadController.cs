using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Authorization;
using Squabby.Database;
using Squabby.Helpers;
using Squabby.Helpers.Authentication;
using Squabby.Models;
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
                .Include(x=>x.Comments) // TODO move to api
                .Include(x=>x.Board)
                .FirstOrDefaultAsync(x => x.Id == threadId && x.Board.Name == name);

            if (thread == null)
                return this.Message($"Could not find a thread with the id {threadId}",
                    $"Thread with the name {threadId} does not exists");
            
            return View("~/Views/Board/Thread/Overview.cshtml", thread);
        }
        
        [SquabbyAuthorize]
        //[HttpPost] TODO
        [Route("PostThread")]
        public async Task<IActionResult> PostThread(short boardId, Models.Thread thread)
        {
            if (string.IsNullOrWhiteSpace(thread?.Title)
                || thread.Title.Length > Models.Thread.MaxTitleLength
                || thread.Content?.Length > Models.Thread.MaxContentLength)
                return View("~/Views/Board/CreateBoard.cshtml", new Error(ErrorType.InvalidParameters)); // TODO

            await using var db = new SquabbyContext();
            var board = await db.Boards.FindAsync(boardId);
            if (board == null)
                return this.Message($"Could not find board {boardId}", $"Board with the name {boardId} does not exists"); // TODO

            var user = HttpContext.GetUser();
            db.Attach(user);
            thread.Owner = user;
            thread.Board = board;
            thread.Rating = 0;
            await db.Threads.AddAsync(thread);
            await db.SaveChangesAsync();
            return this.Message($"Created thread {thread.Title}"); // TODO
        }
        
        [SquabbyAuthorize]
        //[HttpPost] TODO
        [Route("PostComment")]
        public async Task<IActionResult> PostComment(short boardId, int threadId, Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Content)
                || comment.Content.Length > Comment.MaxContentLength)
                return View("~/Views/Board/CreateBoard.cshtml", new Error(ErrorType.InvalidParameters)); // TODO
            
            await using var db = new SquabbyContext();
            var thread = await db.Threads.FindAsync(threadId, boardId);
            if (thread == null) return this.Message($"Could not find thread {boardId}/{threadId}", $"Thread with the id {boardId}/{threadId} does not exists"); //TODO

            var user = HttpContext.GetUser();
            db.Attach(user);
            comment.Owner = user;
            comment.Thread = thread;
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
            return this.Message($"Created comment {comment.Content}"); // TODO
        }
    }
}