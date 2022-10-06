// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Detection.Mocks;

internal static class MockServer
{
    private static RequestDelegate ContextHandler
        => context => context.Response.WriteAsync("Detection:");

    #region WebApplicationBuilder

    internal static WebApplicationBuilder WebApplicationBuilder()
    {
        return WebApplication.CreateBuilder();
    }

    #endregion

    #region Server

    internal static TestServer Server()
    {
        return Server();
    }

    internal static TestServer Server(IWebHostBuilder builder)
    {
        return new TestServer(builder);
    }

    internal static TestServer Server(Action<DetectionOptions> options)
    {
        return Server(WebHostBuilderDetection(options));
    }

    #endregion

    #region WebHostBuilder

    internal static IWebHostBuilder WebHostBuilder()
    {
        return WebHostBuilder(ContextHandler);
    }

    internal static IWebHostBuilder WebHostBuilder(RequestDelegate contextHandler)
    {
        return new WebHostBuilder()
               .ConfigureServices(services => { })
               .Configure(app => { app.Run(contextHandler); });
    }

    internal static IWebHostBuilder WebHostBuilderDetection()
    {
        return WebHostBuilderDetection(options => { });
    }

    internal static IWebHostBuilder WebHostBuilderDetection(Action<DetectionOptions> options)
    {
        return WebHostBuilderDetection(ContextHandler, options);
    }

    internal static IWebHostBuilder WebHostBuilderDetection(RequestDelegate contextHandler)
    {
        return WebHostBuilderDetection(contextHandler, options => { });
    }

    private static IWebHostBuilder WebHostBuilderDetection(RequestDelegate contextHandler, Action<DetectionOptions> options)
    {
        return new WebHostBuilder()
               .ConfigureServices(services =>
               {
                   // var accessor = MockService.MockHttpContextAccessor(agent);
                   // services.TryAddSingleton<IHttpContextAccessor, accessor>();
                   services.AddScoped(sp => sp.GetService<IHttpContextAccessor>().HttpContext.Session);
                   services.AddDetection(options);
               })
               .Configure(app =>
               {
                   app.UseSession();
                   app.UseDetection();
                   app.Run(contextHandler);
               });
    }

    #endregion
}