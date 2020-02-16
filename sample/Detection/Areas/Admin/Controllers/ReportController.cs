using Microsoft.AspNetCore.Mvc;

namespace Detection.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}