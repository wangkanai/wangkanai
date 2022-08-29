// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Markdown.Builder;

public class MarkdownBuilder : IMarkdownBuilder
{
    public IServiceCollection Services { get; }

    public MarkdownBuilder(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }
}