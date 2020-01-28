using Microsoft.AspNetCore.Mvc;

namespace Wangkanai.Detection.Areas.Detection.Controllers
{
    [Area("Detection")]
    public class Preference : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Prefer()
        {
            return View();
        }

        public IActionResult Clear()
        {
            return View();
        }
    }
}