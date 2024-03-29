// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Options;

using Wangkanai.Markdown.DependencyInjection.Options;
using Wangkanai.Markdown.ApplicationModels;

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