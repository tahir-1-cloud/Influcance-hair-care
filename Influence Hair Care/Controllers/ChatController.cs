using Microsoft.AspNetCore.Mvc;

namespace Influence_Hair_Care.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerChat()
        {
            return View();
        }

        public IActionResult SaleRepChat()
        {
            return View();
        }
    }
}
