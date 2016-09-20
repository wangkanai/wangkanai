using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var engine = Request.Engine();
            return View(_client);
        }
    }
}
