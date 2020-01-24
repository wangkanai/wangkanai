// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
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
            builder.Services.TryAddScoped<IPreferenceService, PreferenceService>();
            builder.AddViewLocation(ViewLocationFormat.Suffix);
            builder.AddViewLocation(ViewLocationFormat.Subfolder);

            return builder;
        }

        #region options

        // Do we even need this? because the AddDetection() already has ResponsiveOptions as property in DetectionOptions
        [Obsolete("Not sure the use of this feature yet")]
        public static IDetectionBuilder AddResponsiveService(this IDetectionBuilder builder, Action<ResponsiveOptions> setAction)
        {
            builder.Services.Configure(setAction);
            return builder.AddResponsiveService();
        }

        [Obsolete("Not sure the use of this feature yet")]
        public static IDetectionBuilder AddResponsiveService(this IDetectionBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<ResponsiveOptions>(configuration);
            return builder.AddResponsiveService();
        }

        #endregion

        private static IDetectionBuilder AddViewLocation(this IDetectionBuilder builder, ViewLocationFormat format)
        {
            builder.Services.Configure<RazorViewEngineOptions>(
                options => options.ViewLocationExpanders.Add(new ViewLocationExpander(format))
            );

            return builder;
        }
    }
}