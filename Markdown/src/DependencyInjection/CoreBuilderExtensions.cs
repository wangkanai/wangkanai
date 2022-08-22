// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Detection;
using Wangkanai.Markdown.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class MarkdownCoreBuilderExtensions
{
    public static IMarkdownBuilder AddRequiredServices(this IMarkdownBuilder builder)
    {
        // Hosting doesn't add IHttpContextAccessor by default
        builder.Services.AddHttpContextAccessor();

        // Add Detection Options
        builder.Services.AddOptions();
        builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<DetectionOptions>>().Value);

        return builder;
    }

    public static IMarkdownBuilder AddCoreServices(this IMarkdownBuilder builder)
    {
        // Add basic core to services

        return builder;
    }

    public static IMarkdownBuilder AddMarkerService(this IMarkdownBuilder builder)
    {
        builder.Services.TryAddSingleton<MarkdownMarkerService>();

        return builder;
    }
}