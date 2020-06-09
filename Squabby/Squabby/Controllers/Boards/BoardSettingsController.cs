using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Models;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.Boards
{
    public class BoardSettingsController : Controller
    {
        [Route("CreateBoard")]
        public async Task<ViewResult> CreateBoard(string boardName)
        {
            await using var db = new SquabbyContext();
            if(await db.Boards.AnyAsync(x=>x.Name == boardName))
                return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Could not create board {boardName}", $"Board with the name {boardName} already exists"));

            await db.AddAsync(new Board {Name = boardName, Owner = HttpContext.GetUser() });
            await db.SaveChangesAsync();
            return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Created board {boardName}"));
        } 
    }
}