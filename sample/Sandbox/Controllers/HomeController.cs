using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection.Abstractions;

namespace Sandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientInfo _client;        

        public HomeController(IClientInfo client)
        {
            _client = client;            
        }

        public IActionResult Index()
        {
            var device = Request.Device();
            var browser = Request.Browser();
            var engine = Request.Engine();
            var platform = Request.Platform();
            return View(_client);
        }
    }
}
