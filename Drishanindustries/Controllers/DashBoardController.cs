using Drishanindustries.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.Repository.Product;

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
