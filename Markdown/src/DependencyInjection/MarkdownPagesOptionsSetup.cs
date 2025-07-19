// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.Options;

using Wangkanai.Markdown.ApplicationModels;
using Wangkanai.Markdown.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection;

internal sealed class MarkdownPagesOptionsSetup : IConfigureOptions<MarkdownPagesOptions>
{
	private readonly IServiceProvider _serviceProvider;

	public MarkdownPagesOptionsSetup(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public void Configure(MarkdownPagesOptions options)
	{
		options.ThrowIfNull();
		options.Conventions = new MarkdownConventionCollection(_serviceProvider);
	}
}
