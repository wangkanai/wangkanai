// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using Wangkanai.Federation;
using Wangkanai.Federation.Hosting;
using Wangkanai.Federation.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Pipeline extension methods for adding Federation</summary>
public static class FederationApplicationExtensions
{
	/// <summary>Add Federation to <see cref="IApplicationBuilder" /> request execution pipeline</summary>
	/// <param name="app">The applications.</param>
	/// <returns>Return the <see cref="IApplicationBuilder"/> for further pipeline</returns>
	public static IApplicationBuilder UseFederation(this IApplicationBuilder app)
	{
		app.ThrowIfNull();

		app.Validate();
		app.VerifyMarkerIsRegistered<FederationMarkerService>();
		app.VerifyEndpointRoutingMiddlewareIsNotRegistered(UseFederation);

		var options = app.ApplicationServices.GetRequiredService<FederationOptions>();
		ValidationOptions(options);

		app.UseMiddleware<FederationMiddleware>();

		return app;
	}

	private static void ValidationOptions(FederationOptions options) { }

	private static void Validate(this IApplicationBuilder app)
	{
		var version = typeof(FederationApplicationExtensions).GetVersion();

		var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
		loggerFactory.ThrowIfNull();

		var logger = loggerFactory.CreateLogger("Federation.Startup");
		logger.LogInformation("Starting Federation version {version}", version);
	}
}
