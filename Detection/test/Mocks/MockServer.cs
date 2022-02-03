// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Mocks;

internal static class MockServer
{
    #region Server

    internal static TestServer Server()
        => Server();

    internal static TestServer Server(IWebHostBuilder builder)
        => new(builder);

    internal static TestServer Server(Action<DetectionOptions> options)
        => Server(WebHostBuilderDetection(options));

    #endregion

    #region WebApplicationBuilder

    internal static WebApplicationBuilder WebApplicationBuilder()
        => WebApplication.CreateBuilder();

    #endregion

    #region WebHostBuilder

    internal static IWebHostBuilder WebHostBuilder()
        => WebHostBuilder(ContextHandler);

    internal static IWebHostBuilder WebHostBuilder(RequestDelegate contextHandler)
        => new WebHostBuilder()
           .ConfigureServices(services => { })
           .Configure(app => { app.Run(contextHandler); });

    internal static IWebHostBuilder WebHostBuilderDetection()
        => WebHostBuilderDetection(options => { });

    internal static IWebHostBuilder WebHostBuilderDetection(Action<DetectionOptions> options)
        => WebHostBuilderDetection(ContextHandler, options);

    internal static IWebHostBuilder WebHostBuilderDetection(RequestDelegate contextHandler)
        => WebHostBuilderDetection(contextHandler, options => { });

    private static IWebHostBuilder WebHostBuilderDetection(RequestDelegate contextHandler, Action<DetectionOptions> options)
        => new WebHostBuilder()
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

    #endregion

    private static RequestDelegate ContextHandler
        => context => context.Response.WriteAsync("Detection:");
}