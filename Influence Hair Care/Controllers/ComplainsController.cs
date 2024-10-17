using Microsoft.AspNetCore.Mvc;

namespace Influence_Hair_Care.Controllers
{
    public class ComplainsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerReturns()
        {
            return View();
        }

        public IActionResult SaleRepReturns()
        {
            return View();
        }
    }
}
