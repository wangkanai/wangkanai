// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

using Wangkanai.Detection;

namespace Sandbox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBrowserResolver _browser;

        public HomeController(IBrowserResolver browser)
        {
            _browser = browser;
        }

        public IActionResult Index()
        {
            return View(_browser);
        }
    }
}
