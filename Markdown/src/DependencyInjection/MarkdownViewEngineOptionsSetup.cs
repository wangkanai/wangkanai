// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

using Microsoft.Extensions.Options;

using Wangkanai.Markdown;
using Wangkanai.Markdown.DependencyInjection.Options;
using Wangkanai.Markdown.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

internal sealed class MarkdownViewEngineOptionsSetup : IConfigureOptions<MarkdownViewEngineOptions>
{
	private readonly MarkdownPagesOptions _pagesOptions;

	public MarkdownViewEngineOptionsSetup(IOptions<MarkdownPagesOptions> pagesOptions)
	{
		_pagesOptions = pagesOptions?.Value ?? throw new ArgumentNullException(nameof(pagesOptions));
	}

	public void Configure(MarkdownViewEngineOptions options)
	{
		options.ThrowIfNull();

		var rootDirectory = _pagesOptions.RootDirectory;
		Debug.Assert(!string.IsNullOrEmpty(rootDirectory));
		var defaultPageSearchPath = CombinePath(rootDirectory, "{1}/{0}" + MarkdownViewEngine.ViewExtension);
		options.PageViewLocationFormats.Add(defaultPageSearchPath);

		// /Markdowns/Shared/{0}.md
		var pagesSharedDirectory = CombinePath(rootDirectory, "Shared/{0}" + MarkdownViewEngine.ViewExtension);
		options.PageViewLocationFormats.Add(pagesSharedDirectory);
		options.PageViewLocationFormats.Add("/Views/Shared/{0}" + MarkdownViewEngine.ViewExtension);

		var areaDirectory = CombinePath("/Areas/", "{2}");
		// Areas/{2}/Markdowns/
		var areaPagesDirectory = CombinePath(areaDirectory, "/Markdowns/");

		// Areas/{2}/Markdowns/{1}/{0}.md
		// Areas/{2}/Markdowns/Shared/{0}.md
		// Areas/{2}/Views/Shared/{0}.md
		// Markdowns/Shared/{0}.md
		// Views/Shared/{0}.md
		var areaSearchPath = CombinePath(areaPagesDirectory, "{1}/{0}" + MarkdownViewEngine.ViewExtension);
		options.AreaPageViewLocationFormats.Add(areaSearchPath);

		var areaPagesSharedSearchPath = CombinePath(areaPagesDirectory, "Shared/{0}" + MarkdownViewEngine.ViewExtension);
		options.AreaPageViewLocationFormats.Add(areaPagesSharedSearchPath);
		options.AreaPageViewLocationFormats.Add(pagesSharedDirectory);
		options.AreaPageViewLocationFormats.Add("/Views/Shared/{0}" + MarkdownViewEngine.ViewExtension);

		options.ViewLocationFormats.Add(pagesSharedDirectory);
		options.AreaViewLocationFormats.Add(pagesSharedDirectory);

		options.ViewLocationExpanders.Add(new MarkdownViewLocationExpander());
	}

	private static string CombinePath(string path1, string path2)
	{
		if (path1.EndsWith("/", StringComparison.Ordinal) || path2.StartsWith("/", StringComparison.Ordinal))
			return path1 + path2;
		if (path1.EndsWith("/", StringComparison.Ordinal) && path2.StartsWith("/", StringComparison.Ordinal))
			return string.Concat(path1, path2.AsSpan(1));

		return path1 + "/" + path2;
	}
}