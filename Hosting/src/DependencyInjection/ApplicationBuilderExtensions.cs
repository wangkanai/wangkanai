// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Builder;

using Wangkanai;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
	public static void ValidateOption<T>(this IApplicationBuilder app, T option)
		where T : class
	{
		app.ThrowIfNull();
		option.ThrowIfNull();
	}
	
	public static void VerifyMarkerIsRegistered<T>(this IApplicationBuilder app)
		where T : class
	{
		app.ThrowIfNull();
		
		var type = typeof(T);
		if (app.ApplicationServices.GetService(type) is null)
			throw new InvalidOperationException($"{type.Name} is not added to ConfigureServices(...)");
	}
	
	public static void VerifyEndpointRoutingMiddlewareIsNotRegistered(this IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> middleware)
	{
		middleware.ThrowIfNull();
		
		var EndpointRouteBuilder = "__EndpointRouteBuilder";
		if (app.Properties.TryGetValue(EndpointRouteBuilder, out var obj))
			throw new InvalidOperationException($"{nameof(middleware)} must be in execution pipeline before {nameof(EndpointRoutingApplicationBuilderExtensions.UseRouting)} to 'Configure(...)' in the application startup code.");
	}
}