using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Authorization;
using Squabby.Database;
using Squabby.Helpers;
using Squabby.Helpers.Authentication;
using Squabby.Models;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.Boards.Thread.Post
{
    [Route("b")]
    [Route("board")]
    public class PostController : Controller
    {
        /// <summary>
        /// Create new thread
        /// </summary>
        [SquabbyAuthorize]
        //[HttpPost] TODO
        [Route("{name}/{threadId}/PostComment")]
        public async Task<IActionResult> PostComment(string name, int threadId, Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Title)
                || string.IsNullOrWhiteSpace(comment.Content)
                || comment.Title.Length > Models.Comment.MaxTitleLength
                || comment.Content.Length > Models.Comment.MaxContentLength)
                return View("~/Views/Board/CreateBoard.cshtml", new Error(ErrorType.InvalidParameters)); // TODO
            
            await using var db = new SquabbyContext();
            var thread = await db.Threads.SingleOrDefaultAsync(x=> x.Board.Name == name && x.Id == threadId);
            if (thread == null) return this.Message($"Could not find thread {name}/{threadId}", $"Thread with the id {name}/{threadId} does not exists");

            var user = HttpContext.GetUser();
            db.Attach(user);
            comment.Owner = user;
            comment.Thread = thread;
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
            return this.Message($"Created comment {comment.Title}"); // TODO
        }
    }
}