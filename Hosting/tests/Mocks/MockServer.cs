// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace Wangkanai.Hosting.Mocks;

internal static class MockServer
{
	private static readonly RequestDelegate ContextHandler
		= context => context.Response.WriteAsync("Hosting:");

	internal static TestServer Server(IWebHostBuilder builder)
		=> new TestServer(builder);

	internal static IWebHostBuilder WebHostBuilder()
		=> WebHostBuilder(ContextHandler);

	internal static IWebHostBuilder WebHostBuilder(RequestDelegate contextHandler)
		=> new WebHostBuilder()
		   .ConfigureServices(services => { })
		   .Configure(app => { app.Run(contextHandler); });

}
