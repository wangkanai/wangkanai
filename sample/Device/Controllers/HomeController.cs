using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection;
using Wangkanai.Detection.Abstractions;

namespace Sandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDeviceResolver _device;        

        public HomeController(IDeviceResolver device)
        {
            _device = device;
        }

        public IActionResult Index()
        {            
            return View(_device.Device);
        }
    }
}
