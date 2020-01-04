// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Pipeline extension methods for adding Detection
    /// </summary>
    public static class DetectionApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds Detection to the pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseDetection(this IApplicationBuilder app)
        {
            app.Validate();

            return app;
        }

        internal static void Validate(this IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger("Detection.Startup");
            logger.LogInformation("Starting Detection version {version}", typeof(DetectionApplicationBuilderExtensions).Assembly.GetName().Version.ToString());
        }
    }
}
