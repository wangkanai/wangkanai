// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Markdown.Builder;

public sealed class MarkdownBuilder : IMarkdownBuilder
{
	public MarkdownBuilder(IServiceCollection services)
	{
		Services = services ?? throw new ArgumentNullException(nameof(services));
	}

	public IServiceCollection Services { get; }
}