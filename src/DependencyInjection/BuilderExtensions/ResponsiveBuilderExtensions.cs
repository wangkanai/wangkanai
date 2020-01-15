// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Mvc.Razor;

using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Responsive;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponsiveBuilderExtensions
    {
        public static IDetectionCoreBuilder AddResponsiveService(
            this IDetectionCoreBuilder builder)
            => builder.AddResponsiveService(options => { });

        public static IDetectionCoreBuilder AddResponsiveService(
            this IDetectionCoreBuilder builder,
            Action<ResponsiveOptions> setAction)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.Configure<ResponsiveOptions>(setAction);
            builder.AddViewLocation(ResponsiveViewLocationFormat.Suffix);
            builder.AddViewLocation(ResponsiveViewLocationFormat.Subfolder);

            return builder;
        }

        private static IDetectionCoreBuilder AddViewLocation(
            this IDetectionCoreBuilder builder,
            ResponsiveViewLocationFormat format)
        {
            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(format));
            });

            return builder;
        }
    }
}
