// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponsiveCollectionExtensions
    {
        public static IDetectionCoreBuilder AddResponsive(
            this IDetectionCoreBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            return builder;
        }

        public static IDetectionCoreBuilder AddResponsive(
            this IDetectionCoreBuilder builder,
            Action<ResponsiveOptions> options)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));


            return builder;
        }
    }
}
