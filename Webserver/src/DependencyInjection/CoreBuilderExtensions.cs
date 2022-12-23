// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Webserver.Services;

namespace Microsoft.Extensions.DependencyInjection;

internal static class CoreBuilderExtensions
{
    public static IWebserverBuilder AddRequiredServices(this IWebserverBuilder builder)
    {
        builder.IfNullThrow();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddOptions();
        builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<WebserverOptions>>().Value);

        return builder;
    }

    public static IWebserverBuilder AddCoreServices(this IWebserverBuilder builder)
    {
        builder.Services.TryAddScoped<IWebserverService, WebserverService>();

        return builder;
    }

    public static IWebserverBuilder AddMarkerService(this IWebserverBuilder builder)
    {
        builder.Services.TryAddSingleton<WebserverMarkerService, WebserverMarkerService>();

        return builder;
    }
}