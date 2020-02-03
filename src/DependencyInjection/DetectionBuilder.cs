// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Helper functions for configuring detection services.
    /// </summary>
    public class DetectionBuilder : IDetectionBuilder
    {
        /// <summary>
        ///     Creates a new instance of <see cref="DetectionBuilder" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
        public DetectionBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public DetectionBuilder(IServiceCollection services, DetectionOptions options)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        ///     Gets the <see cref="IServiceCollection" /> services are attached to.
        /// </summary>
        /// <value>
        ///     The <see cref="IServiceCollection" /> services are attached to.
        /// </value>
        public IServiceCollection Services { get; }
        
        public DetectionOptions Options { get; }
    }
}