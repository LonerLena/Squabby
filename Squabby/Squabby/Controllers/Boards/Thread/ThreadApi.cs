using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Authorization;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Models;

namespace Squabby.Controllers.Boards.Thread
{
    // TODO clean up code
    [Route("/api/b")]
    [Route("/api/board")]
    public class ThreadApi : Controller
    {
        [SquabbyAuthorize]
        [HttpPost]
        [Route("{name}/Like")]
        public async Task<JsonResult> LikeThread(Models.Thread thread) => await Rate(thread, 1);

        [SquabbyAuthorize]
        [HttpPost]
        [Route("{name}/Dislike")]
        public async Task<JsonResult> DislikeThread(Models.Thread thread) => await Rate(thread, -1);

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

            
            thread = new Models.Thread {Id = t.Id, BoardId = t.BoardId};
            if (t.UserRating == null)
            {
                AddRating(thread, db, ratingValue);
                await db.Ratings.AddAsync(new Rating {UserId = user.Id, BoardId = t.BoardId, ThreadId = t.Id, Value = ratingValue});
            }
            else
            {
                AddRating(thread, db, (short)(ratingValue + t.UserRating.Value * -1));
                t.UserRating.Value = ratingValue;
                db.Ratings.Update(t.UserRating);
            }

            await db.SaveChangesAsync();
            return Json(new {status = "success"});
        }

        private static void AddRating(Models.Thread thread, SquabbyContext db, short value)
        {
            if(value == 0) return;
            thread.Rating += value;
            db.Threads.Attach(thread).Property(x=>x.Rating).IsModified = true;
        }
    }
}