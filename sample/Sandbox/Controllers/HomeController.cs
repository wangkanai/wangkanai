// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

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
            IUserAgentService useragentService,
            IDeviceResolver deviceResolver,
            IBrowserResolver browserResolver,
            IEngineResolver engineResolver,
            IPlatformResolver platformResolver,
            ICrawlerResolver crawlerResolver)
        {
            client = new ClientInfo
            {
                UserAgent = useragentService.UserAgent,
                Device = deviceResolver.Device,
                Browser = browserResolver.Browser,
                Engine = engineResolver.Engine,
                Platform = platformResolver.Platform,
                Crawler = crawlerResolver.Crawler
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
        public ICrawler Crawler { get; set; }
    }
}
