using APP.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        // go to /SeedDb to reset the data
        [Route("SeedDb")]
        public IActionResult Seed()
        {
            var dbFactory = new DbFactory();
            dbFactory.SeedDb();
            return Content("<h1>Database seed successful.</h1>", "text/html", Encoding.UTF8);
        }
    }
}
