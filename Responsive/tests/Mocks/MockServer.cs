// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Detection.Models;
using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Mocks;

internal static class MockServer
{
	private static RequestDelegate ContextHandler
		=> context => context.GetDevice() switch
		{
			Device.Desktop => context.Response.WriteAsync("desktop"),
			Device.Tablet  => context.Response.WriteAsync("tablet"),
			Device.Mobile  => context.Response.WriteAsync("mobile"),
			Device.Watch   => context.Response.WriteAsync("watch"),
			Device.Tv      => context.Response.WriteAsync("tv"),
			Device.Console => context.Response.WriteAsync("console"),
			Device.Car     => context.Response.WriteAsync("car"),
			_              => context.Response.WriteAsync("who?")
		};

	internal static TestServer Server()
	{
		return Server(WebHostBuilder());
	}

	internal static TestServer Server(Action<ResponsiveOptions> options)
	{
		return Server(WebHostBuilder(options));
	}

	internal static TestServer Server(IWebHostBuilder builder)
	{
		return new TestServer(builder);
	}


	internal static IWebHostBuilder WebHostBuilder()
	{
		return WebHostBuilder(ContextHandler);
	}

	internal static IWebHostBuilder WebHostBuilder(Action<ResponsiveOptions> options)
	{
		return WebHostBuilder(ContextHandler, options);
	}

	internal static IWebHostBuilder WebHostBuilder(RequestDelegate contextHandler)
	{
		return WebHostBuilder(contextHandler, _ => { });
	}


	private static IWebHostBuilder WebHostBuilder(RequestDelegate contextHandler, Action<ResponsiveOptions> options)
	{
		return new WebHostBuilder()
		       .ConfigureServices(services => {
			       services.TryAddSingleton<IHttpContextAccessor, MockHttpContextAccessor>();
			       services.AddSession();
			       services.AddDetection();
			       services.AddResponsive(options);
		       })
		       .Configure(app => {
			       app.UseDetection();
			       app.UseSession();
			       app.UseResponsive();
			       app.Run(contextHandler);
		       });
	}
}