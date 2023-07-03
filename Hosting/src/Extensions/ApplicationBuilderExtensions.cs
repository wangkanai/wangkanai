// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Builder;

using Wangkanai;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
	public static void ValidateOption<T>(this IApplicationBuilder app, T option)
		where T : class { }
	
	public static bool VerifyMarkerIsRegistered<T>(this IApplicationBuilder app)
		where T : class
	{
		var type = typeof(T);
		return app.ApplicationServices.GetService(type) is null 
			       ? throw new InvalidOperationException($"{type.Name} is not added to ConfigureServices(...)") 
			       : true;
	}
	
	public static void VerifyEndpointRoutingMiddlewareIsNotRegistered(this IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> middleware)
	{
		middleware.ThrowIfNull();
		
		var EndpointRouteBuilder = "__EndpointRouteBuilder";
		if (app.Properties.TryGetValue(EndpointRouteBuilder, out var obj))
			throw new InvalidOperationException($"{nameof(middleware)} must be in execution pipeline before {nameof(EndpointRoutingApplicationBuilderExtensions.UseRouting)} to 'Configure(...)' in the application startup code.");
	}
}