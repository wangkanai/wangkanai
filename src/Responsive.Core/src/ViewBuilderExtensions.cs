// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Mvc.Razor;

using Wangkanai.Responsive;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ViewBuilderExtensions
    {
        public static IResponsiveCoreBuilder AddViewSuffix(
            this IResponsiveCoreBuilder builder)
        {
            if (builder == null)
                throw new ViewBuilderExtensionsSuffixArgumentNullException(nameof(builder));

            return AddViewLocation(builder, ResponsiveViewLocationFormat.Suffix);
        }

        public static IResponsiveCoreBuilder AddViewSubfolder(
            this IResponsiveCoreBuilder builder)
        {
            if (builder == null)
                throw new ViewBuilderExtensionsSubfolderArgumentNullException(nameof(builder));

            return AddViewLocation(builder, ResponsiveViewLocationFormat.Subfolder);
        }

        private static IResponsiveCoreBuilder AddViewLocation(
            this IResponsiveCoreBuilder builder,
            ResponsiveViewLocationFormat format)
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
