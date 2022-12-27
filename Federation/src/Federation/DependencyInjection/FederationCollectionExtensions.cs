// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods to <see cref="IServiceCollection"/> for configuring client services.
/// </summary>
public static class FederationCollectionExtensions
{
    /// <summary>
    /// Add Federation service to the services container
    /// </summary>
    /// <param name="services">The services available in the application</param>
    /// <returns>An <see cref="IFederationBuilder"/> so that additional calls can be chained</returns>
    public static IFederationBuilder AddFederation(this IServiceCollection services)
        => services.AddFederationBuilder()
                   .AddRequiredServices()
                   .AddCoreServices()
                   .AddMarkerService();

    public static IFederationBuilder AddFederation(this IServiceCollection services, Action<FederationOptions> setAction)
        => services.Configure(setAction)
                   .AddFederation();
    
    internal static IFederationBuilder AddFederationBuilder(this IServiceCollection services)
        => new FederationBuilder(services);
}