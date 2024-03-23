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
        [Route("Home/product-details/{product_name?}")]
        [Route("Home/product-details")]
        public IActionResult Product_Details(string product_name)
        {
            return View("FrontEnd/product_details");
        }
        public IActionResult products()
        {
            return View("FrontEnd/products");
        }

    }
}
