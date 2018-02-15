// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection.Builder;

namespace Wangkanai.Detection
{
    public static class DetectionCollectionExtensions
    {
        public static IDetectionBuilder AddDetection(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDetectionCore()
                .AddDevice()
                .AddBrowser()
                .AddPlatform()
                .AddEngine()
                .AddCrawler();

            return new DetectionBuilder(services);
        }
    }
}
