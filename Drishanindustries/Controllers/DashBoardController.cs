using Microsoft.AspNetCore.Mvc;

namespace Drishanindustries.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
