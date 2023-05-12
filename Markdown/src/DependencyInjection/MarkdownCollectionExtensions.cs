// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Markdown.Builder;
using Wangkanai.Markdown.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Contains extension method to <see cref="IServiceCollection" /> for configuring markdown services.
/// </summary>
public static class MarkdownCollectionExtensions
{
    public static IMarkdownBuilder AddMarkdownPages(this IServiceCollection services)
    {
        return services.AddMarkdownBuilder()
                       .AddRequiredServices()
                       .AddCoreServices()
                       .AddMarkerService();
    }

    public static IMarkdownBuilder AddMarkdownPages(this IServiceCollection services, Action<MarkdownPagesOptions> setAction)
    {
        return services.Configure(setAction)
                       .AddMarkdownPages();
    }

    // For internal unit tests
    internal static IMarkdownBuilder AddMarkdownBuilder(this IServiceCollection services)
    {
        return new MarkdownBuilder(services);
    }
}