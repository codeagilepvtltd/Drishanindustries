using Microsoft.AspNetCore.Mvc;

namespace Drishanindustries.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View("FrontEnd/Index");
        }

    }
}
