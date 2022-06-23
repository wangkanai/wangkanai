// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using Wangkanai.Detection;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Pipeline extension methods for adding Detection
/// </summary>
public static class DetectionApplicationExtensions
{
    /// <summary>
    /// Adds Detection to <see cref="IApplicationBuilder" /> request execution pipeline.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <returns>Return the <see cref="IApplicationBuilder" /> for further pipeline</returns>
    public static IApplicationBuilder UseDetection(this IApplicationBuilder app)
    {
        app = Check.NotNull(app);

        app.Validate();
        app.VerifyMarkerIsRegistered<DetectionMarkerService>();
        app.VerifyEndpointRoutingMiddlewareIsNotRegistered(UseDetection);

        var options = app.ApplicationServices.GetRequiredService<DetectionOptions>();
        ValidateOptions(options);

        return app;
    }

    private static void ValidateOptions(DetectionOptions options)
    {
    }

    private static void Validate(this IApplicationBuilder app)
    {
        var version = typeof(DetectionApplicationExtensions)?.Assembly?.GetName()?.Version?.ToString();

        var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
        Check.NotNull(loggerFactory);

        var logger = loggerFactory.CreateLogger("Detection.Startup");
        logger.LogInformation("Starting Detection version {Version}", version);

        //var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

        //using (var scope = scopeFactory.CreateScope())
        //{
        //    var serviceProvider = scope.ServiceProvider;

        //    var options = serviceProvider.GetRequiredService<DetectionOptions>();
        //    ValidateOptions(options);
        //}
    }
}