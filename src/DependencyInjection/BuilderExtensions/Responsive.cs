// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Detection.DependencyInjection.Options;
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
            
            // For future development and exploration
            //builder.Services.AddRazorPageLocation();

            return builder;
        }

        public static IDetectionBuilder AddSessionServices(this IDetectionBuilder builder)
        {
            // Add Session to services
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(
                options =>
                {
                    options.Cookie.Name        = "Detection";
                    options.IdleTimeout        = TimeSpan.FromSeconds(10);
                    options.Cookie.IsEssential = true;
                });

            return builder;
        }

        private static IServiceCollection AddRazorViewLocation(this IServiceCollection services)
            => services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix));
                options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Subfolder));
            });

        private static IServiceCollection AddRazorPageLocation(this IServiceCollection services)
            => services.Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AddPageRoute("", "");
                options.Conventions.AddPageRoute("", "");
            });
    }
}