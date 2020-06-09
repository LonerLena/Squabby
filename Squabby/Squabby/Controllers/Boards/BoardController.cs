using Microsoft.AspNetCore.Mvc;

namespace Squabby.Controllers.Boards
{
    [Route("board")]
    [Route("b")]
    public class BoardController
    {
        [Route("show")]
        public string BoardIndex()
        {
            return "Board index";
        }
    }
}