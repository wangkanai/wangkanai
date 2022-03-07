// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Webmaster.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class CoreCollectionExtensions
{
    public static IWebmasterBuilder AddCoreServices(this IWebmasterBuilder builder)
    {
        builder.Services.TryAddSingleton<IWebmasterService, WebmasterService>();

        return builder;
    }

    public static IWebmasterBuilder AddMarkerService(this IWebmasterBuilder builder)
    {
        builder.Services.TryAddSingleton<WebmasterMarkerService, WebmasterMarkerService>();

        return builder;
    }
}