// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Wangkanai.Web.Server.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
	public string? RequestId { get; set; }

	public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

	private readonly ILogger<ErrorModel> _logger;

	public ErrorModel(ILogger<ErrorModel> logger)
	{
		_logger = logger;
	}

	public void OnGet()
	{
		RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
	}
}