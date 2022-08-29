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
        => Server(WebHostBuilder());

    public static TestServer Server(Action<AnalyticsOptions> options)
        => Server(WebHostBuilder(options));

    public static TestServer Server(IWebHostBuilder builder)
        => new TestServer(builder);

    public static IWebHostBuilder WebHostBuilder()
        => WebHostBuilder(options => { });

    public static IWebHostBuilder WebHostBuilder(Action<AnalyticsOptions> options)
        => new WebHostBuilder()
           .ConfigureServices(services => services.AddAnalytics(options))
           .Configure(app => { app.Run(ContextHandler()); });

    private static RequestDelegate ContextHandler()
        => context => context.Response.WriteAsync("Analytics");
}