using Microsoft.AspNetCore.Mvc;

namespace Detection.Controllers
{
    public class AboutController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}