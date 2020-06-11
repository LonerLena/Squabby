using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Authorization;
using Squabby.Database;
using Squabby.Helpers.Authentication;
using Squabby.Models;
using Squabby.Models.ViewModels;

namespace Squabby.Controllers.Boards
{
    [SquabbyAuthorize]
    public class BoardSettingsController : Controller
    {
        [Route("CreateBoard")]
        public IActionResult CreateBoard() => View("~/Views/Board/CreateBoard.cshtml");
        
        /// <summary>
        /// Create new board
        /// </summary>
        [HttpPost]
        [Route("CreateBoard")]
        public async Task<IActionResult> CreateBoard(Board board)
        {
            if (string.IsNullOrWhiteSpace(board?.Name) || board.Description.Length > Board.MaxDescriptionLength) 
                return View("~/Views/Board/CreateBoard.cshtml", new Error(ErrorType.InvalidParameters));
            
            await using var db = new SquabbyContext();
            if (await db.Boards.AnyAsync(x => x.Name == board.Name))
                return View("~/Views/Board/CreateBoard.cshtml", new Error(ErrorType.NameAlreadyUsedError));

            board.Owner = await db.Users.SingleOrDefaultAsync(x => x.Username == HttpContext.GetUser().Username);
            await db.AddAsync(board);
            await db.SaveChangesAsync();
            return Redirect($"/b/{board.Name}");
        } 
    }
}