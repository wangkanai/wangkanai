// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Detection;
using Wangkanai.Detection.Models;
using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Mocks;

internal static class MockServer
{
    #region Server

    internal static TestServer Server()
        => Server(WebHostBuilder());

    internal static TestServer Server(Action<ResponsiveOptions> options)
        => Server(WebHostBuilder(options));

    internal static TestServer Server(IWebHostBuilder builder)
        => new(builder);

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

    internal static IWebHostBuilder WebHostBuilder(Action<ResponsiveOptions> options)
        => WebHostBuilder(ContextHandler, options);

    private static IWebHostBuilder WebHostBuilder(RequestDelegate contextHandler, Action<ResponsiveOptions> options)
        => new WebHostBuilder()
           .ConfigureServices(services =>
           {
               services.AddHttpContextAccessor();
               services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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