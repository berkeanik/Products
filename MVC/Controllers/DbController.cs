using APP.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        // seeds the database with initial data, e.g. https://localhost:7069/SeedDb
        [Route("SeedDb")]
        public IActionResult Seed()
        {
            var dbFactory = new DbFactory();
            dbFactory.SeedDb();
            return Content("<h1>Database seed successful.</h1>", "text/html", Encoding.UTF8);
        }
    }
}
