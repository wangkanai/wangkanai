using System;
using System.Collections.Generic;
using System.Text;
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
