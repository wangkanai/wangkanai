// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Hosting;

[Area(AreaName)]
public class PreferenceController : Controller
{
    private readonly IResponsiveService _responsive;
    private const    string             AreaName = "Detection";

    public PreferenceController(IResponsiveService responsive)
        => _responsive = responsive;

    // GET
    public IActionResult Index()
        => Content("Preference");

    // GET
    public IActionResult Prefer(string? returnUrl = null)
    {
        _responsive.PreferSet(Device.Desktop);

        return LocalRedirect(returnUrl ?? "/");
    }

    // GET
    public IActionResult Clear(string? returnUrl = null)
    {
        _responsive.PreferClear();

        return LocalRedirect(returnUrl ?? "/");
    }
}