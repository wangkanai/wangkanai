// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using Wangkanai.Federation.Hosting;
using Wangkanai.Federation.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseFederation(this IApplicationBuilder app)
    {
        app.ThrowIfNull();

        app.Validate();
        app.VerifyMarkerIsRegistered();

        app.UseMiddleware<FederationMiddleware>();

        return app;
    }

    private static void Validate(this IApplicationBuilder app)
    {
        var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
        loggerFactory.ThrowIfNull();

        var logger = loggerFactory.CreateLogger("Federation.Startup");
        logger.LogInformation("Starting Federation version {version}", typeof(ApplicationBuilderExtensions).GetVersion());
    }

    private static void VerifyMarkerIsRegistered(this IApplicationBuilder app)
    {
        app.ApplicationServices
           .GetService(typeof(FederationMarkerService))
           .ThrowIfNull<InvalidOperationException>($"{nameof(FederationCollectionExtensions.AddFederation)} is not added to configure services(...)");
    }
}