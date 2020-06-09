using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squabby.Database;

namespace Squabby.Controllers.Boards
{
    /*
    [Route("b")]
    [Route("board")]
    public class BoardController
    {
        [Route("{board}")]
        public async Task<ViewResult> Profile(string boardName)
        {
            await using var db = new SquabbyContext();
            var board = await db.Boards.FirstOrDefaultAsync(x => x.Name == boardName);
            //if (user == null) return error; TODO
            return View(board);
        }
    }*/
}