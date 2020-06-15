using System;
using System.Linq;
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
                .Include(x => x.Owner)
                .Include(x => x.Comments)
                .SingleOrDefaultAsync(x => x.Board.Name == name && x.Id == threadId);

            if (thread == null)
                return this.Message($"Could not find a thread with the id {threadId}",
                    $"Thread with the name {threadId} does not exists");
            
            return View("~/Views/Board/Thread/Overview.cshtml", thread);
        }

        /// <summary>
        /// Create new thread
        /// </summary>
        [SquabbyAuthorize]
        //[HttpPost] TODO
        [Route("{name}/Post")]
        public async Task<IActionResult> PostThread(string name, Models.Thread thread)
        {
            if (string.IsNullOrWhiteSpace(thread?.Title)
                || thread.Title.Length > Models.Thread.MaxTitleLength
                || thread.Content?.Length > Models.Thread.MaxContentLength)
                return View("~/Views/Board/CreateBoard.cshtml", new Error(ErrorType.InvalidParameters)); // TODO

            await using var db = new SquabbyContext();
            var board = await db.Boards.SingleOrDefaultAsync(x => x.Name == name);
            if (board == null)
                return this.Message($"Could not find board {name}", $"Board with the name {name} does not exists");

            var user = HttpContext.GetUser();
            db.Attach(user);
            thread.Owner = user;
            thread.Board = board;
            await db.Threads.AddAsync(thread);
            await db.SaveChangesAsync();
            return this.Message($"Created thread {thread.Title}"); // TODO
        }

        [SquabbyAuthorize]
        [HttpPost]
        [Route("{name}/Like")]
        public async Task<IActionResult> LikeThread(Models.Thread thread)
        {
            return await Rate(thread, 1);
        }

        [SquabbyAuthorize]
        [HttpPost]
        [Route("{name}/Dislike")]
        public async Task<IActionResult> DislikeThread(Models.Thread thread)
        {
            return await Rate(thread, -1);
        }

        private async Task<JsonResult> Rate(Models.Thread thread, short ratingValue)
        {
            await using var db = new SquabbyContext();
            var user = HttpContext.GetUser();

            var t = await db.Threads.Select(t => new
            {
                t.Id,
                t.BoardId,
                t.Rating,
                UserRating = t.Ratings.FirstOrDefault(rating =>
                    rating.Owner.Id == user.Id && rating.BoardId == thread.BoardId && rating.ThreadId == thread.Id)
            }).FirstOrDefaultAsync(t => t.BoardId == thread.BoardId && t.Id == thread.Id);

            if (t.UserRating != null)
            {
                t.UserRating.Value = ratingValue;
                db.Ratings.Update(t.UserRating);
            }
            else db.Ratings.Add(new Rating {UserId = user.Id, BoardId = t.BoardId, ThreadId = t.Id, Value = ratingValue});
            
            thread = new Models.Thread {Id = t.Id, BoardId = t.BoardId, Rating = t.Rating + ratingValue};
            db.Threads.Attach(thread);
            db.Entry(thread).Property(x => x.Rating).IsModified = true;

            await db.SaveChangesAsync();
            return Json(new {status = "success"});
        }
    }
}