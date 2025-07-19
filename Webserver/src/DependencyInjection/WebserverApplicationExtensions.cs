// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;

using Wangkanai.Webserver.Hosting;
using Wangkanai.Webserver.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebserverApplicationExtensions
{
	public static IApplicationBuilder UseWebserver(this IApplicationBuilder app)
	{
		app.ThrowIfNull();
		app.Validate();
		app.VerifyMarkerIsRegistered<WebserverMarkerService>();

		return app.UseMiddleware<WebserverMiddleware>();
	}

	private static void Validate(this IApplicationBuilder app)
	{
		// what should I validate?
	}
}
