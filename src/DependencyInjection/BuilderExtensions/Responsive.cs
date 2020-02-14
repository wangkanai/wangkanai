// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Detection.Hosting;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponsiveBuilderExtensions
    {
        public static IDetectionBuilder AddResponsiveService(this IDetectionBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.TryAddTransient<IResponsiveService, ResponsiveService>();
            builder.Services.AddRazorViewLocation();
            builder.Services.AddRazorPagesLocation();

            return builder;
        }

        public static IDetectionBuilder AddSessionServices(this IDetectionBuilder builder)
        {
            // Add Session to services
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(
                options =>
                {
                    options.Cookie.Name = "Detection";
                    options.IdleTimeout = TimeSpan.FromSeconds(10);
                    options.Cookie.IsEssential = true;
                });

            return builder;
        }

        private static IServiceCollection AddRazorViewLocation(this IServiceCollection services)
            => services.Configure<RazorViewEngineOptions>(options =>
            {
                //options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix));
                //options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Subfolder));
                options.ViewLocationExpanders.Add(new ResponsivePageLocationExpander());
            });

        private static IServiceCollection AddRazorPagesLocation(this IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.Conventions.Add(new ResponsivePageRouteModelConvention());
            });
            services.AddSingleton<MatcherPolicy, ResponsivePageMatcherPolicy>();
            
            return services;
        }
    }
}