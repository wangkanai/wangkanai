// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

using Wangkanai.Responsive.Core.Internal;

namespace Wangkanai.Responsive
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
            if (app == null)
                throw new UseResponsiveArgumentNullException(nameof(app));

            return app.UseResponsive(options => { });
        }

        /// <summary>
        /// Adds the responsive to <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns>Return the <see cref="IApplicationBuilder"/> for further pipeline</returns>
        [Obsolete("This method will soon be deprecated. Use UseResponsive(options => {}) instead.")]
        public static IApplicationBuilder UseResponsive(
            this IApplicationBuilder app,
            ResponsiveOptions options)
        {
            if (app == null)
                throw new UseResponsiveAppArgumentNullException(nameof(app));
            if (options == null)
                throw new UseResponsiveOptionArgumentNullException(nameof(options));

            VerifyResponsiveIsRegistered(app);

            return app.UseMiddleware<ResponsiveMiddleware>(Options.Create(options));
        }

        /// <summary>
        /// Adds the responsive to <see cref="IApplicationBuilder"/> request execution pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <returns>Return the <see cref="IApplicationBuilder"/> for further pipeline</returns>
        public static IApplicationBuilder UseResponsive(
            this IApplicationBuilder app,
            Action<IResponsiveOptionsBuilder> options)
        {
            if (app == null)
                throw new UseResponsiveAppArgumentNullException(nameof(app));
            if (options == null)
                throw new UseResponsiveOptionArgumentNullException(nameof(options));

            VerifyResponsiveIsRegistered(app);

            return app.UseMiddleware<ResponsiveMiddleware>();
        }

        private static void VerifyResponsiveIsRegistered(IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetService(typeof(ResponsiveMarkerService)) == null)
                throw new InvalidOperationException("AddResponsive() is not added to ConfigureServices(...)");
        }
    }
}
