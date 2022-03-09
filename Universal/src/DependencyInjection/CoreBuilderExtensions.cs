// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Universal.Options;
using Wangkanai.Universal.Services;

namespace Microsoft.Extensions.DependencyInjection;

internal static class CoreBuilderExtensions
{
    public static IUniversalBuilder AddRequiredPlatformServices(this IUniversalBuilder builder)
    {
        Check.NotNull(builder);

        builder.Services.AddOptions();
        builder.Services.TryAddSingleton(provider => ServiceProviderServiceExtensions.GetRequiredService<IOptions<UniversalOption>>(provider).Value);

        return builder;
    }

    public static IUniversalBuilder AddCoreServices(this IUniversalBuilder builder)
    {
        // Add Basic core to services
        builder.Services.TryAddScoped<ITrackerService, TrackerService>();

        return builder;
    }

    public static IUniversalBuilder AddMarkerService(this IUniversalBuilder builder)
    {
        builder.Services.TryAddSingleton<UniversalMarkerService>();

        return builder;
    }
}