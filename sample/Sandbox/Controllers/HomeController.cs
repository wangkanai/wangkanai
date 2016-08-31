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
        private readonly IClientService _client;

        public HomeController(IClientService client)
        {
            _client = client;
        }

        public IActionResult Index()
        {
            var browser = HttpContext.Request.Browser();
            var device = HttpContext.Request.Device();
            var platform = HttpContext.Request.Platform();            
            var engine = HttpContext.Request.Engine();
            return View(_client);
        }
    }
}
