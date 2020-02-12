// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Hosting;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Pipeline extension methods for adding Detection
    /// </summary>
    public static class DetectionApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds Detection to <see cref="IApplicationBuilder" /> request execution pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>Return the <see cref="IApplicationBuilder" /> for further pipeline</returns>
        public static IApplicationBuilder UseDetection(this IApplicationBuilder app)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));

            app.Validate();

            VerifyMarkerIsRegistered(app);

            var options = app.ApplicationServices.GetRequiredService<DetectionOptions>();

            ValidateOptions(options);

            if (options.Responsive.Disable)
                return app;

            if (options.Responsive.IncludeWebApi)
                return app.UseResponsive();

            return app.UseWhen(
                context => !context.Request.Path.StartsWithSegments(options.Responsive.WebApiPath),
                appBuilder => appBuilder.UseResponsive()
            );
        }

        private static IApplicationBuilder UseResponsive(this IApplicationBuilder app)
        {
            app.UseSession();
            app.UseMiddleware<ResponsiveMiddleware>();
            return app;
        }

        private static void Validate(this IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            if (loggerFactory is null)
                throw new ArgumentNullException(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger("Detection.Startup");
            logger.LogInformation("Starting Detection version {version}",
                typeof(DetectionApplicationBuilderExtensions)?.Assembly?.GetName()?.Version?.ToString());

            //var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            //using (var scope = scopeFactory.CreateScope())
            //{
            //    var serviceProvider = scope.ServiceProvider;

            //    var options = serviceProvider.GetRequiredService<DetectionOptions>();
            //    ValidateOptions(options);
            //}
        }

        private static void ValidateOptions(DetectionOptions options)
        {
            if (options.Responsive.Disable && options.Responsive.IncludeWebApi)
                throw new InvalidOperationException("IncludeWebApi is not needed if already Disable");
        }

        private static void VerifyMarkerIsRegistered(IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetService(typeof(DetectionMarkerService)) == null)
                throw new InvalidOperationException("AddDetection() is not added to ConfigureServices(...)");
        }
    }
}