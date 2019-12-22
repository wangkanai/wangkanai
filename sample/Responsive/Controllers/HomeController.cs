// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;

namespace Responsive.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();//_resolver.DeviceInfo);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
