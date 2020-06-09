using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.Boards
{
    [Route("b")]
    [Route("board")]
    public class BoardController : Controller
    {
        [Route("{boardName}")]
        public async Task<ViewResult> Overview(string boardName)
        {
            await using var db = new SquabbyContext();
            var board = await db.Boards.FirstOrDefaultAsync(x => x.Name == boardName);
            if (board == null) 
                return View("~/Views/Home/CustomError.cshtml", new Message(MessageType.Error, $"Could not find board {boardName}", $"Board with the name {boardName} does not exists"));
            return View(board);
        }
    }
}