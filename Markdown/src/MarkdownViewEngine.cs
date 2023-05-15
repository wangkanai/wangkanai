// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Wangkanai.Markdown.DependencyInjection.Options;
using Wangkanai.Mvc.Routing;

namespace Wangkanai.Markdown;

public partial class MarkdownViewEngine : IMarkdownViewEngine
{
	public static readonly string ViewExtension = ".md";

	private const string AreaKey       = "area";
	private const string ControllerKey = "controller";
	private const string PageKey       = "page";
	private const string MarkdownKey   = "markdown";

	private static readonly TimeSpan _cacheExpirationDuration = TimeSpan.FromMinutes(20);

	private readonly IMarkdownPageFactoryProvider _pageFactory;
	private readonly IMarkdownPageActivator       _pageActivator;
	private readonly HtmlEncoder                  _htmlEncoder;
	private readonly ILogger                      _logger;
	private readonly MarkdownViewEngineOptions    _options;
	private readonly DiagnosticListener           _diagnosticListener;

	public MarkdownViewEngine(
		IMarkdownPageFactoryProvider        pageFactory,
		IMarkdownPageActivator              pageActivator,
		HtmlEncoder                         htmlEncoder,
		IOptions<MarkdownViewEngineOptions> optionsAccessor,
		ILoggerFactory                      loggerFactory,
		DiagnosticListener                  diagnosticListener)
	{
		_options = optionsAccessor.Value;

		if (_options.ViewLocationFormats.Count == 0)
		{
			throw new ArgumentException(
				string.Format(Resources.ViewLocationFormatsIsRequired,
				              nameof(MarkdownViewEngineOptions.ViewLocationFormats),
				              nameof(optionsAccessor)));
		}

		if (_options.AreaViewLocationFormats.Count == 0)
		{
			throw new ArgumentException(
				string.Format(Resources.ViewLocationFormatsIsRequired,
				              nameof(MarkdownViewEngineOptions.AreaViewLocationFormats),
				              nameof(optionsAccessor)));
		}

		_pageFactory        = pageFactory;
		_pageActivator      = pageActivator;
		_htmlEncoder        = htmlEncoder;
		_logger             = loggerFactory.CreateLogger<RazorViewEngine>();
		_diagnosticListener = diagnosticListener;
		ViewLookupCache     = new MemoryCache(new MemoryCacheOptions());
	}

	protected internal IMemoryCache ViewLookupCache { get; private set; }

	internal void ClearCache()
		=> ViewLookupCache = new MemoryCache(new MemoryCacheOptions());

	public static string? GetNormalizedRouteValue(ActionContext context, string key)
		=> NormalizedRouteValue.GetNormalizedRouteValue(context, key);

	public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
	{
		throw new NotImplementedException();
	}

	public ViewEngineResult GetView(string? executingFilePath, string viewPath, bool isMainPage)
	{
		throw new NotImplementedException();
	}

	public MarkdownPageResult FindPage(ActionContext context, string pageName)
	{
		throw new NotImplementedException();
	}

	public MarkdownPageResult GetPage(string executingFilePath, string pagePath)
	{
		throw new NotImplementedException();
	}

	public string? GetAbsolutePath(string? executingFilePath, string? pagePath)
	{
		throw new NotImplementedException();
	}
}