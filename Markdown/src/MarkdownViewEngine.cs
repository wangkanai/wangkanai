// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Wangkanai.Markdown;

public partial class MarkdownViewEngine : IMarkdownViewEngine
{
	/// <summary>
	/// The view extension
	/// </summary>
	public static readonly string ViewExtension = ".md";
	
	private const string AreaKey       = "area";
	private const string ControllerKey = "controller";
	private const string PageKey       = "page";
	
	private static readonly TimeSpan _cacheExpirationDuration = TimeSpan.FromMinutes(20);
	
	public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
	{
		throw new NotImplementedException();
	}

	public ViewEngineResult GetView(string? executingFilePath, string viewPath, bool isMainPage)
	{
		throw new NotImplementedException();
	}
}