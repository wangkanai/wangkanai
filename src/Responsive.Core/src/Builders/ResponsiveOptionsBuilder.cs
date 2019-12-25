// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;

namespace Wangkanai.Responsive
{
    public class ResponsiveOptionsBuilder : IResponsiveOptionsBuilder
    {
        public IApplicationBuilder ApplicationBuilder { get; }
        public IServiceProvider ServiceProvider { get; }
        //public IViewLocation DefaultHandler { get; set; }

        public ResponsiveOptionsBuilder(
            IApplicationBuilder applicationBuilder)
        {
            ApplicationBuilder = applicationBuilder
                ?? throw new ArgumentNullException(nameof(applicationBuilder));

            ServiceProvider = ApplicationBuilder.ApplicationServices;
        }

        [Obsolete("This method will be remove in 2.0-beta14")]
        public ResponsiveOptionsBuilder(
            IApplicationBuilder applicationBuilder,
            IViewLocation defaultHandler)
        {
            ApplicationBuilder = applicationBuilder
                ?? throw new ArgumentNullException(nameof(applicationBuilder));
            //DefaultHandler = defaultHandler
            //    ?? throw new ArgumentNullException(nameof(applicationBuilder));

            ServiceProvider = ApplicationBuilder.ApplicationServices;
        }

    }
}
