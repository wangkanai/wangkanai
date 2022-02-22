// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

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
        Check.NotNull(app);

        app.Validate();

        app.VerifyMarkerIsRegistered();

        app.VerifyEndpointRoutingMiddlewareIsNotRegistered();

        var options = app.ApplicationServices.GetRequiredService<DetectionOptions>();

        ValidateOptions(options);

        return app;
    }

    private static void ValidateOptions(DetectionOptions options)
    {
    }

    private static void Validate(this IApplicationBuilder app)
    {
        var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
        Check.NotNull(loggerFactory);

        var logger = loggerFactory.CreateLogger("Detection.Startup");
        logger.LogInformation("Starting Detection version {version}",
                              typeof(DetectionApplicationExtensions)?.Assembly?.GetName()?.Version?.ToString());

        //var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

        //using (var scope = scopeFactory.CreateScope())
        //{
        //    var serviceProvider = scope.ServiceProvider;

        //    var options = serviceProvider.GetRequiredService<DetectionOptions>();
        //    ValidateOptions(options);
        //}
    }


    private static void VerifyEndpointRoutingMiddlewareIsNotRegistered(this IApplicationBuilder app)
    {
        var EndpointRouteBuilder = "__EndpointRouteBuilder";
        if (app.Properties.TryGetValue(EndpointRouteBuilder, out var obj))
            throw new InvalidOperationException($"{nameof(UseDetection)} must be in execution pipeline before {nameof(EndpointRoutingApplicationBuilderExtensions.UseRouting)} to 'Configure(...)' in the application startup code.");
    }

    private static void VerifyMarkerIsRegistered(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetService(typeof(DetectionMarkerService)) == null)
            throw new InvalidOperationException($"{nameof(DetectionCollectionExtensions.AddDetection)} is not added to ConfigureServices(...)");
    }
}