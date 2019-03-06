// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Responsive
{
    public class ResponsiveBuilder : IResponsiveBuilder
    {
        public IServiceCollection Services { get; }
        public ResponsiveBuilder(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            Services = services;
        }
    }
}