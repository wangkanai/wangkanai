// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static bool VerifyMarkerIsRegistered<T>(this IApplicationBuilder app)
        where T : class
    {
        var type = typeof(T);
        var name = type.Name;
        if (app.ApplicationServices.GetService(type) is null)
            throw new InvalidOperationException($"{name} is not added to ConfigureServices(...)");
        return true;
    }

    public static void ValidateOption<T>(this IApplicationBuilder app, T option)
        where T : class
    {
    }

    public static void VerifyEndpointRoutingMiddlewareIsNotRegistered(this IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> middleware)
    {
        var EndpointRouteBuilder = "__EndpointRouteBuilder";
        if (app.Properties.TryGetValue(EndpointRouteBuilder, out var obj))
            throw new InvalidOperationException($"{nameof(middleware)} must be in execution pipeline before {nameof(EndpointRoutingApplicationBuilderExtensions.UseRouting)} to 'Configure(...)' in the application startup code.");
    }
}