// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using Wangkanai.Analytics.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Pipeline extension methods for adding Analytics
/// </summary>
public static class AnalyticsApplicationExtensions
{
    public static IApplicationBuilder UseAnalytics(this IApplicationBuilder app)
    {
        Check.NotNull(app);

        //app.Validate();
        
        app.VerifyMarkerIsRegistered();

        return app;
    }

    private static void Validate(this IApplicationBuilder app)
    {
        var factory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
        //Check.NotNull(factory);

        var logger = factory.CreateLogger("Analytics.Startup");
        logger.LogInformation("Starting Analytics version {version}", typeof(AnalyticsApplicationExtensions)?.Assembly?.GetName()?.Version?.ToString());
    }

    private static void VerifyMarkerIsRegistered(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetService(typeof(AnalyticsMarkerService)) == null)
            throw new InvalidOperationException($"{nameof(AnalyticsCollectionExtensions.AddAnalytics)} is not added to ConfigureServices(...)");
    }
}