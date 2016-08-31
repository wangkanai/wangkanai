using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Browser;

namespace Sandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrowserService _browser;

        public HomeController(IBrowserService browser)
        {
            _browser = browser;
        }

        public IActionResult Index()
        {
            var browser = HttpContext.Request.Browser();
            var device = HttpContext.Request.Device();
            var platform = HttpContext.Request.Platform();            
            var engine = HttpContext.Request.Engine();
            return View(_browser);
        }
    }
}
