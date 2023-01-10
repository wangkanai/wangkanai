// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Analytics.Tests.Mocks;

public static class MockServer
{
	public static TestServer Server()
	{
		return Server(WebHostBuilder());
	}

	public static TestServer Server(Action<AnalyticsOptions> options)
	{
		return Server(WebHostBuilder(options));
	}

	public static TestServer Server(IWebHostBuilder builder)
	{
		return new TestServer(builder);
	}

	public static IWebHostBuilder WebHostBuilder()
	{
		return WebHostBuilder(options => { });
	}

	public static IWebHostBuilder WebHostBuilder(Action<AnalyticsOptions> options)
	{
		return new WebHostBuilder()
		       .ConfigureServices(services => services.AddAnalytics(options))
		       .Configure(app => { app.Run(ContextHandler()); });
	}

	private static RequestDelegate ContextHandler()
	{
		return context => context.Response.WriteAsync("Analytics");
	}
}