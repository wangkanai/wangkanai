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
        private readonly ClientInfo client;
        
        public HomeController(
            IDetectionService detectionService,
            IDeviceResolver deviceResolver, 
            IBrowserResolver browserResolver, 
            IEngineResolver engineResolver, 
            IPlatformResolver platformResolver)
        {
            client = new ClientInfo
            {
                UserAgent = detectionService.UserAgent,
                Device = deviceResolver.Device,
                Browser = browserResolver.Browser,
                Engine = engineResolver.Engine,
                Platform = platformResolver.Platform
            };
        }

        public IActionResult Index()
        {
            return View(client);
        }        
    }

    public class ClientInfo
    {
        public IUserAgent UserAgent { get; set; }
        public IDevice Device { get; set; }
        public IBrowser Browser { get; set; }
        public IEngine Engine { get; set; }
        public IPlatform Platform { get; set; }
    }
}
