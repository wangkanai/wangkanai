using Microsoft.AspNetCore.Mvc;

namespace ResponsiveView.Areas.Admin.Controllers
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