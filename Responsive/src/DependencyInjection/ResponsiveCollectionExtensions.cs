// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Responsive;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Contains extensions method to <see cref="IServiceCollection" /> for configuring client services
/// </summary>
public static class ResponsiveCollectionExtensions
{
    /// <summary>
    ///     Add Responsive service to the services container
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IResponsiveBuilder AddResponsive(this IServiceCollection services)
    {
        return services.AddResponsiveBuilder()
                       .AddRequiredPlatformServices()
                       .AddCoreServices()
                       .AddSessionServices()
                       .AddResponsiveService();
    }

    /// <summary>
    ///     Add Responsive service to the service container
    /// </summary>
    /// <param name="services">The services available in the application</param>
    /// <param name="setAction">
    ///     An <see cref="Action{ResposnsiveOptions}" /> to configure the provided
    ///     <see cref="ResponsiveOptions" />
    /// </param>
    /// <returns>An <see cref="IResponsiveBuilder" /> so that additional calls can be chained</returns>
    public static IResponsiveBuilder AddResponsive(this IServiceCollection services, Action<ResponsiveOptions> setAction)
    {
        return services.Configure(setAction)
                       .AddResponsive();
    }

    private static IResponsiveBuilder AddResponsiveBuilder(this IServiceCollection services)
    {
        return new ResponsiveBuilder(services);
    }
}