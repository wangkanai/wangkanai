// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;

namespace Detection.Areas.Admin.Controllers;

[Area("Admin")]
public class ReportController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}