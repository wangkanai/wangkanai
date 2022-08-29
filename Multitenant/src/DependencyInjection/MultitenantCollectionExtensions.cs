// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Multitenant.Options;
using Wangkanai.Webserver;

namespace Microsoft.Extensions.DependencyInjection;

public static class MultiTenantCollectionExtensions
{
    public static IMultiTenantBuilder AddMultitenant(this IServiceCollection services)
        => services.AddMultitenantBuilder()
                   .AddRequiredServices()
                   .AddCoreServices()
                   .AddMarkerService();

    public static IMultiTenantBuilder AddMultiTenant(this IServiceCollection services, Action<MultiTenantOption> setAction)
        => services.Configure(setAction)
                   .AddMultitenant();

    internal static IMultiTenantBuilder AddMultitenantBuilder(this IServiceCollection services)
        => new MultiTenantBuilder(services);
}