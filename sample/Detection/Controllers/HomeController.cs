// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//using Wangkanai.Detection;

namespace Sandbox.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IDetection _detection;

        public HomeController()//IDetection detection)
        {
            //_detection = detection;
        }

        public IActionResult Index()
        {
            return View();// _detection);
        }
    }
}
