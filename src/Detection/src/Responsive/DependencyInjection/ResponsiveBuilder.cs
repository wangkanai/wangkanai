// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ResponsiveBuilder : IResponsiveBuilder
    {
        public IServiceCollection Services { get; }

        public ResponsiveBuilder(IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            Services = services;
        }
    }
}
