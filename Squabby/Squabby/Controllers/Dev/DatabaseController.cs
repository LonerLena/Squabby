using Microsoft.AspNetCore.Mvc;
using Squabby.Database;

namespace Squabby.Controllers.Dev
{
    #if DEBUG
    public class DevelopController : Controller
    {
        [Route("/dev/CreateDatabase")]
        public string CreateDatabase()
        {
            using (var db = new DatabaseContext()) db.Database.EnsureCreated();
            return "success";
        }
    }
    #endif
}