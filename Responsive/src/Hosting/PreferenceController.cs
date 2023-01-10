// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;

using Wangkanai.Detection.Models;
using Wangkanai.Responsive.Services;

namespace Wangkanai.Responsive.Hosting;

[Area(AreaName)]
public sealed class PreferenceController : Controller
{
	private const    string             AreaName = "Responsive";
	private readonly IResponsiveService _responsive;

	public PreferenceController(IResponsiveService responsive)
	{
		_responsive = responsive;
	}

	// GET
	public IActionResult Index()
	{
		return Content("Preference");
	}

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