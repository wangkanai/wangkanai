// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;

namespace Wangkanai.Responsive
{
    public class ResponsiveOptionsBuilder : IResponsiveOptionsBuilder
    {
        public IApplicationBuilder ApplicationBuilder { get; }
        public IServiceProvider ServiceProvider
        {
            get { return ApplicationBuilder.ApplicationServices; }
        }
    }
}
