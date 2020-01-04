// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wangkanai.Responsive;
using Wangkanai.Responsive.Core.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding the <see cref="ResponsiveMiddleware"/> to an application.
    /// </summary>
    public static class ResponsiveApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the Responsive to <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>Return the <see cref="IApplicationBuilder"/> for further pipeline</returns>
        public static IApplicationBuilder UseResponsive(
            this IApplicationBuilder app)
        {
            if (app is null)
                throw new UseResponsiveAppArgumentNullException(nameof(app));

            app.Validate();

            VerifyResponsiveIsRegistered(app);

            app.UseMiddleware<ResponsiveMiddleware>();

            return app;
        }

        internal static void Validate(this IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger("Responsive.Startup");
            logger.LogInformation("Starting Responsive version {version}", typeof(ResponsiveApplicationBuilderExtensions).Assembly.GetName().Version.ToString());

            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var options = serviceProvider.GetRequiredService<ResponsiveOptions>();
                ValidateOptions(options);
            }
        }

        private static void ValidateOptions(ResponsiveOptions options)
        {
            //if (options.View.IsConfigured())

            // What should I validate?
        }

        private static void VerifyResponsiveIsRegistered(IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetService(typeof(ResponsiveMarkerService)) == null)
                throw new InvalidOperationException("AddResponsive() is not added to ConfigureServices(...)");
        }
    }
}
