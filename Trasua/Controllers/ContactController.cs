using Microsoft.AspNetCore.Mvc;

namespace Trasua.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
