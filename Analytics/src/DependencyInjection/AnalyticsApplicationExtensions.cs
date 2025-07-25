// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Builder;

using Wangkanai.Analytics.Hosting;
using Wangkanai.Analytics.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Pipeline extension methods for adding Analytics</summary>
public static class AnalyticsApplicationExtensions
{
	/// <summary>Adds Analytics to <see cref="IApplicationBuilder" /> request execution pipeline.</summary>
	/// <param name="app">The application.</param>
	/// <returns>Return the <see cref="IApplicationBuilder" /> for further pipeline</returns>
	public static IApplicationBuilder UseAnalytics(this IApplicationBuilder app)
	{
		app.ThrowIfNull();

		app.Validate();
		app.VerifyMarkerIsRegistered<AnalyticsMarkerService>();

		app.UseMiddleware<AnalyticsMiddleware>();

		return app;
	}

	private static void Validate(this IApplicationBuilder app)
	{
		//var factory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
		//Check.NotNull(factory);

		// var logger = factory.CreateLogger("Analytics.Startup");
		// logger.LogInformation("Starting Analytics version {version}", typeof(AnalyticsApplicationExtensions)?.Assembly?.GetName()?.Version?.ToString());
	}
}
