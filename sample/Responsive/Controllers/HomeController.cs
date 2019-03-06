// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection;
using Wangkanai.Responsive;

namespace Sandbox.Controllers
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
