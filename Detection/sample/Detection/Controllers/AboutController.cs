// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;

namespace Detection.Controllers
{
    public class AboutController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}