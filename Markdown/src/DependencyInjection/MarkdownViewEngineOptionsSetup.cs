// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Options;

using Wangkanai.Markdown.DependencyInjection.Options;

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
		throw new NotImplementedException();
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