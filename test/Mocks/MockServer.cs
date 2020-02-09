// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection
{
    internal static class MockServer
    {
        public static TestServer CreateServer()
            => CreateServer(CreateWebHostBuilder());

        public static TestServer CreateServer(Action<DetectionOptions> options)
            => CreateServer(CreateWebHostBuilder(options));

        public static TestServer CreateServer(IWebHostBuilder builder)
            => new TestServer(builder);

        public static IWebHostBuilder CreateWebHostBuilder()
            => CreateWebHostBuilder(options => { });

        public static IWebHostBuilder CreateWebHostBuilder(Action<DetectionOptions> options)
            => new WebHostBuilder()
               .ConfigureServices(services =>
                   services.AddDetection(options))
               .Configure(app =>
               {
                   app.UseDetection();
                   app.Run(ResponsiveContextHandler());
               });

        private static RequestDelegate ResponsiveContextHandler()
        {
            return context => context.GetDevice() switch
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
    }
}