// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Hosting;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponsiveBuilderExtensions
    {
        public static IDetectionBuilder AddResponsive(this IDetectionBuilder builder)
        {
            return builder.AddResponsive(options => { });
        }

        public static IDetectionBuilder AddResponsive(this IDetectionBuilder builder, Action<ResponsiveOptions> setAction)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.Configure(setAction);

            builder.AddViewLocation(ViewLocationFormat.Suffix);
            builder.AddViewLocation(ViewLocationFormat.Subfolder);

            // waiting for development
            builder.Services.TryAddTransient<IResponsiveService, ResponsiveService>();

            return builder;
        }

        private static IDetectionBuilder AddViewLocation(this IDetectionBuilder builder, ViewLocationFormat format)
        {
            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander(format));
            });

            return builder;
        }
    }
}
