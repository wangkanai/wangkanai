// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Responsive;
using Wangkanai.Responsive.Builders;
using Wangkanai.Responsive.Core.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponsiveCoreCollectionExtensions
    {
        public static IResponsiveCoreBuilder AddResponsiveCore(this IServiceCollection services)
        {
            if (services == null)
                throw new AddResponsiveArgumentNullException(nameof(services));

            services.AddDetectionCore()
                .AddDevice();

            services.TryAddSingleton<ResponsiveMarkerService, ResponsiveMarkerService>();

            return new ResponsiveCoreBuilder(services);
        }
    }
}
