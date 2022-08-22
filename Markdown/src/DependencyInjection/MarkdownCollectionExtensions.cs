// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Markdown.Builder;
using Wangkanai.Markdown.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension method to <see cref="IServiceCollection" /> for configuring markdown services.
/// </summary>
public static class MarkdownCollectionExtensions
{
    public static IMarkdownBuilder AddMarkdown(this IServiceCollection services)
        => services.AddMarkdownBuilder()
                   .AddRequiredServices()
                   .AddCoreServices()
                   .AddMarkdownPages()
                   .AddMarkerService();

    public static IMarkdownBuilder AddMarkdown(this IServiceCollection services, Action<MarkdownOptions> setAction)
        => services.Configure(setAction)
                   .AddMarkdown();

    // For internal unit tests
    internal static IMarkdownBuilder AddMarkdownBuilder(this IServiceCollection services)
        => new MarkdownBuilder(services);
}