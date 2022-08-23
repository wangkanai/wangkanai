// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Multitenant.Options;
using Wangkanai.Multitenant.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class MultiTenantCoreBuilderExtensions
{
    public static IMultiTenantBuilder AddRequiredServices(this IMultiTenantBuilder builder)
    {
        builder = Check.NotNull(builder);

        // Hosting doesn't add IHttpContextAccessor by derfault
        builder.Services.AddHttpContextAccessor();

        // Add Multi Tenant options
        builder.Services.AddOptions();
        builder.Services.TryAddSingleton
            (provider => provider.GetRequiredService<IOptions<MultiTenantOption>>().Value);

        return builder;
    }

    public static IMultiTenantBuilder AddCoreServices(this IMultiTenantBuilder builder)
    {
        // Add basic core to services
        //builder.Services.TryAddScoped<>();

        return builder;
    }

    public static IMultiTenantBuilder AddMarkerService(this IMultiTenantBuilder builder)
    {
        builder.Services.TryAddSingleton<MultiTenantMarkerService>();

        return builder;
    }
}