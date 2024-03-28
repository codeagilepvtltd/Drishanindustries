using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ProductCataLog.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("FrontEnd/Index");
        }       

    }
}
