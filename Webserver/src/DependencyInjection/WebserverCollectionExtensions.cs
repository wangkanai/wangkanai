// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

public static class WebserverCollectionExtensions
{
    public static IWebserverBuilder AddWebserver(this IServiceCollection services, Action<WebserverOptions> setAction)
        => services.Configure(setAction)
                   .AddWebserver();

    public static IWebserverBuilder AddWebserver(this IServiceCollection services)
        => services.AddWebserverBuilder()
                   .AddCoreServices()
                   .AddMarkerService();

    private static IWebserverBuilder AddWebserverBuilder(this IServiceCollection services)
        => new WebserverBuilder(services);
}