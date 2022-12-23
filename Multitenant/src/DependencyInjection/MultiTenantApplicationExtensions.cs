// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using Wangkanai.Multitenant.Options;
using Wangkanai.Multitenant.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class MultiTenantApplicationExtensions
{
    public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder app)
    {
        app.IfNullThrow();

        app.ValidateGeneric();
        app.VerifyMarkerIsRegistered<MultiTenantMarkerService>();
        app.VerifyEndpointRoutingMiddlewareIsNotRegistered(UseMultiTenant);


        return app;
    }

    private static void ValidationOptions(MultiTenantOption options)
    {
    }

    private static void ValidateGeneric(this IApplicationBuilder app)
    {
        var version = typeof(MultiTenantApplicationExtensions)?.Assembly?.GetName()?.Version?.ToString();

        var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
        loggerFactory = Check.NotNull(loggerFactory);

        var logger = loggerFactory.CreateLogger("MultiTenant.Startup");
        logger.LogInformation("Starting MultiTenant server {Version}", version);
    }
}