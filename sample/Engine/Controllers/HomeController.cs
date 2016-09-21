using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection;

namespace Sandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEngineResolver _engine;        

        public HomeController(IEngineResolver engine)
        {
            _engine = engine;            
        }

        public IActionResult Index()
        {            
            return View(_engine);
        }
    }
}
