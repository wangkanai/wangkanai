// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Detection;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Mocks;

internal static class MockServer
{
    #region Server

    internal static TestServer Server()
        => Server();

    internal static TestServer Server(IWebHostBuilder builder)
        => new(builder);

    internal static TestServer Server(Action<DetectionOptions> detection)
        => Server();

    internal static TestServer Server(Action<DetectionOptions> detection, Action<ResponsiveOptions> responsive)
        => Server();

    [Obsolete]
    internal static TestServer ServerDetection()
        => Server(WebHostBuilderDetection());

    [Obsolete]
    internal static TestServer ServerDetection(Action<DetectionOptions> options)
        => Server(WebHostBuilderDetection(options));

    [Obsolete]
    internal static TestServer ServerResponsive()
        => Server(WebHostBuilderResponsive());

    [Obsolete]
    internal static TestServer ServerResponsive(Action<ResponsiveOptions> options)
        => Server(WebHostBuilderResponsive(options));

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

    internal static IWebHostBuilder WebHostBuilderResponsive()
        => WebHostBuilderResponsive(options => { });

    internal static IWebHostBuilder WebHostBuilderResponsive(Action<ResponsiveOptions> options)
        => WebHostBuilderResponsive(ContextHandler, options);

    internal static IWebHostBuilder WebHostBuilderResponsive(RequestDelegate contextHandler)
        => WebHostBuilderResponsive(contextHandler, options => { });

    private static IWebHostBuilder WebHostBuilderResponsive(RequestDelegate contextHandler, Action<ResponsiveOptions> options)
        => new WebHostBuilder()
           .ConfigureServices(services =>
           {
               services.AddHttpContextAccessor();
               //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
               services.AddScoped(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Session);
               services.AddDetection();
               services.AddResponsive(options);
           })
           .Configure(app =>
           {
               app.UseDetection();
               app.UseSession();
               app.UseResponsive();
               app.Run(contextHandler);
           });

    #endregion

    private static RequestDelegate ContextHandler
        => context => context.GetDevice() switch
                      {
                          Device.Desktop => context.Response.WriteAsync("Response: Desktop"),
                          Device.Tablet  => context.Response.WriteAsync("Response: Tablet"),
                          Device.Mobile  => context.Response.WriteAsync("Response: Mobile"),
                          Device.Watch   => context.Response.WriteAsync("Response: Watch"),
                          Device.Tv      => context.Response.WriteAsync("Response: TV"),
                          Device.Console => context.Response.WriteAsync("Response: Console"),
                          Device.Car     => context.Response.WriteAsync("Response: Car"),
                          _              => context.Response.WriteAsync("Response: Who?")
                      };
}