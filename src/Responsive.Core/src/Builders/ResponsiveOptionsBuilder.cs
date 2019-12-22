// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;

namespace Wangkanai.Responsive
{
    public class ResponsiveOptionsBuilder : IResponsiveOptionsBuilder
    {
        public ResponsiveOptionsBuilder(
            IApplicationBuilder applicationBuilder)
            : this(applicationBuilder, defaultHandler: null) { }

        public ResponsiveOptionsBuilder(
            IApplicationBuilder applicationBuilder,
            IViewLocation defaultHandler)
        {
            ApplicationBuilder = applicationBuilder
                ?? throw new ArgumentNullException(nameof(applicationBuilder));
            DefaultHandler = defaultHandler
                ?? throw new ArgumentNullException(nameof(applicationBuilder));

            ServiceProvider = ApplicationBuilder.ApplicationServices;
        }

        public IApplicationBuilder ApplicationBuilder { get; }
        public IServiceProvider ServiceProvider { get; }
        public IViewLocation DefaultHandler { get; set; }
    }
}
