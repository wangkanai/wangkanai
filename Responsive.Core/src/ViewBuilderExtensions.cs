// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Wangkanai.Responsive;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ViewBuilderExtensions
    {
        public static IResponsiveBuilder AddViewSuffix(this IResponsiveBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            return AddViewLocation(builder, ResponsiveViewLocationFormat.Suffix);
        }

        public static IResponsiveBuilder AddViewSubfolder(this IResponsiveBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            return AddViewLocation(builder, ResponsiveViewLocationFormat.Subfolder);
        }

        private static IResponsiveBuilder AddViewLocation(this IResponsiveBuilder builder, ResponsiveViewLocationFormat format)
        {
            builder.Services.Configure<RazorViewEngineOptions>(
                options =>
                {
                    options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(format));
                });

            return builder;
        }
    }
}
