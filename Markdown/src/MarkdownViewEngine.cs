// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Globalization;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using Wangkanai.Extensions;
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

	public MarkdownPageResult FindPage(ActionContext context, string pageName)
	{
		context.ThrowIfNull();
		pageName.ThrowIfNullOrEmpty<ArgumentException>(Resources.ArgumentCannotBeNullOrEmpty);

		if (IsApplicationRelativePath(pageName) || IsRelativePath(pageName))
			return new MarkdownPageResult(pageName, Enumerable.Empty<string>());

		var cacheResult = LocatePageFromViewLocations(context, pageName, isMainPage: false);
		if (cacheResult.Success)
		{
			var razorPage = cacheResult.ViewEntry.PageFactory();
			return new MarkdownPageResult(pageName, razorPage);
		}

		return new MarkdownPageResult(pageName, cacheResult.SearchedLocations!);
	}

	private MarkdownViewLocationCacheResult LocatePageFromPath(string? executingFilePath, string pagePath, bool isMainPage)
	{
		var applicationRelativePath = GetAbsolutePath(executingFilePath, pagePath)!;
		var cacheKey                = new MarkdownViewLocationCacheKey(applicationRelativePath, isMainPage);

		if (!ViewLookupCache.TryGetValue(cacheKey, out MarkdownViewLocationCacheResult? cacheResult))
		{
			var expirationTokens = new HashSet<IChangeToken>();
			cacheResult = CreateCacheResult(expirationTokens, applicationRelativePath, isMainPage);

			var cacheEntryOptions = new MemoryCacheEntryOptions();
			cacheEntryOptions.SetSlidingExpiration(_cacheExpirationDuration);
			foreach (var expirationToken in expirationTokens)
				cacheEntryOptions.AddExpirationToken(expirationToken);

			if (cacheResult == null)
				cacheResult = new MarkdownViewLocationCacheResult(new[] { applicationRelativePath });

			cacheResult = ViewLookupCache.Set(
				cacheKey,
				cacheResult,
				cacheEntryOptions);
		}

		return cacheResult!;
	}

	internal MarkdownViewLocationCacheResult? CreateCacheResult(
		HashSet<IChangeToken> expirationTokens,
		string                relativePath,
		bool                  isMainPage)
	{
		var factoryResult  = _pageFactory.CreateFactory(relativePath);
		var viewDescriptor = factoryResult.ViewDescriptor;
		if (viewDescriptor?.ExpirationTokens != null)
		{
			var viewExpirationTokens      = viewDescriptor.ExpirationTokens;
			var viewExpirationTokensCount = viewExpirationTokens.Count;
			for (var i = 0; i < viewExpirationTokensCount; i++)
				expirationTokens.Add(viewExpirationTokens[i]);
		}

		if (factoryResult.Success)
		{
			// Only need to lookup _ViewStarts for the main page.
			var viewStartPages = isMainPage
				                     ? GetViewStartPages(viewDescriptor!.RelativePath, expirationTokens)
				                     : Array.Empty<MarkdownViewLocationCacheItem>();

			return new MarkdownViewLocationCacheResult(
				new MarkdownViewLocationCacheItem(factoryResult.MarkdownPageFactory, relativePath),
				viewStartPages);
		}

		return null;
	}

	private MarkdownViewLocationCacheResult LocatePageFromViewLocations(
		ActionContext actionContext,
		string        pageName,
		bool          isMainPage)
	{
		var controllerName = GetNormalizedRouteValue(actionContext, ControllerKey);
		var areaName       = GetNormalizedRouteValue(actionContext, AreaKey);

		string? razorPageName = null;
		if (actionContext.ActionDescriptor.RouteValues.ContainsKey(PageKey))
			razorPageName = GetNormalizedRouteValue(actionContext, PageKey);

		var expanderContext = new MarkdownViewLocationExpanderContext(
			actionContext,
			pageName,
			controllerName,
			areaName,
			razorPageName,
			isMainPage);
		Dictionary<string, string?>? expanderValues = null;

		var expanders = _options.ViewLocationExpanders;
		// Read interface .Count once rather than per iteration
		var expandersCount = expanders.Count;
		if (expandersCount > 0)
		{
			expanderValues         = new Dictionary<string, string?>(StringComparer.Ordinal);
			expanderContext.Values = expanderValues;

			// Perf: Avoid allocations
			for (var i = 0; i < expandersCount; i++)
				expanders[i].PopulateValues(expanderContext);
		}

		var cacheKey = new MarkdownViewLocationCacheKey(
			expanderContext.ViewName,
			expanderContext.ControllerName,
			expanderContext.AreaName,
			expanderContext.PageName,
			expanderContext.IsMainPage,
			expanderValues);

		if (!ViewLookupCache.TryGetValue<MarkdownViewLocationCacheResult>(cacheKey, out var cacheResult) || cacheResult is null)
		{
			Log.ViewLookupCacheMiss(_logger, cacheKey.ViewName, cacheKey.ControllerName);
			cacheResult = OnCacheMiss(expanderContext, cacheKey);
		}
		else
			Log.ViewLookupCacheHit(_logger, cacheKey.ViewName, cacheKey.ControllerName);

		return cacheResult;
	}

	private MarkdownViewLocationCacheResult OnCacheMiss(
		MarkdownViewLocationExpanderContext expanderContext,
		MarkdownViewLocationCacheKey        cacheKey)
	{
		var viewLocations = GetViewLocationFormats(expanderContext);

		var expanders      = _options.ViewLocationExpanders;
		var expandersCount = expanders.Count;

		for (var i = 0; i < expandersCount; i++)
			viewLocations = expanders[i].ExpandViewLocations(expanderContext, viewLocations);

		MarkdownViewLocationCacheResult? cacheResult = null;

		var searchedLocations = new List<string>();
		var expirationTokens  = new HashSet<IChangeToken>();
		foreach (var location in viewLocations)
		{
			var path = string.Format(
				CultureInfo.InvariantCulture,
				location,
				expanderContext.ViewName,
				expanderContext.ControllerName,
				expanderContext.AreaName);

			path = ViewEnginePath.ResolvePath(path);

			cacheResult = CreateCacheResult(expirationTokens, path, expanderContext.IsMainPage);
			if (cacheResult != null)
				break;

			searchedLocations.Add(path);
		}

		// No views were found at the specified location. Create a not found result.
		if (cacheResult == null)
			cacheResult = new MarkdownViewLocationCacheResult(searchedLocations);

		var cacheEntryOptions = new MemoryCacheEntryOptions();
		cacheEntryOptions.SetSlidingExpiration(_cacheExpirationDuration);
		foreach (var expirationToken in expirationTokens)
			cacheEntryOptions.AddExpirationToken(expirationToken);

		ViewLookupCache.Set(cacheKey, cacheResult, cacheEntryOptions);
		return cacheResult;
	}

	private static bool IsApplicationRelativePath(string name)
	{
		Debug.Assert(!string.IsNullOrEmpty(name));
		return name[0] == '~' || name[0] == '/';
	}

	private static bool IsRelativePath(string name)
	{
		Debug.Assert(!string.IsNullOrEmpty(name));
		return name.EndsWith(ViewExtension, StringComparison.OrdinalIgnoreCase);
	}

	private IReadOnlyList<MarkdownViewLocationCacheItem> GetViewStartPages(
		string                path,
		HashSet<IChangeToken> expirationTokens)
	{
		var viewStartPages = new List<MarkdownViewLocationCacheItem>();

		foreach (var filePath in MarkdownFileHierarchy.GetViewStartPaths(path))
		{
			var result         = _pageFactory.CreateFactory(filePath);
			var viewDescriptor = result.ViewDescriptor;
			if (viewDescriptor?.ExpirationTokens != null)
				for (var i = 0; i < viewDescriptor.ExpirationTokens.Count; i++)
					expirationTokens.Add(viewDescriptor.ExpirationTokens[i]);

			if (result.Success)
				viewStartPages.Insert(0, new MarkdownViewLocationCacheItem(result.MarkdownPageFactory, filePath));
		}

		return viewStartPages;
	}

	internal IEnumerable<string> GetViewLocationFormats(MarkdownViewLocationExpanderContext context)
	{
		if (!context.AreaName.IsNullOrEmpty() && !context.ControllerName.IsNullOrEmpty())
			return _options.AreaViewLocationFormats;

		if (!context.ControllerName.IsNullOrEmpty())
			return _options.ViewLocationFormats;

		if (!context.AreaName.IsNullOrEmpty() && !context.PageName.IsNullOrEmpty())
			return _options.AreaPageViewLocationFormats;

		if (!context.PageName.IsNullOrEmpty())
			return _options.PageViewLocationFormats;

		return _options.ViewLocationFormats;
	}

	public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
	{
		throw new NotImplementedException();
	}

	public ViewEngineResult GetView(string? executingFilePath, string viewPath, bool isMainPage)
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

	private static partial class Log
	{
		[LoggerMessage(1, LogLevel.Debug, "View lookup cache miss for view '{ViewName}' in controller '{ControllerName}'.", EventName = "ViewLookupCacheMiss")]
		public static partial void ViewLookupCacheMiss(ILogger logger, string viewName, string? controllerName);

		[LoggerMessage(2, LogLevel.Debug, "View lookup cache hit for view '{ViewName}' in controller '{ControllerName}'.", EventName = "ViewLookupCacheHit")]
		public static partial void ViewLookupCacheHit(ILogger logger, string viewName, string? controllerName);
	}
}