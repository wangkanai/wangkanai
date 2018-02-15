// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Detection.Builder
{
    /// <summary>
    /// Helper functions for configuring detection core services.
    /// </summary>
    public class DetectionCoreBuilder : IDetectionCoreBuilder
    {
        /// <summary>
        /// Creates a new instance of <see cref="DetectionCoreBuilder"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
        public DetectionCoreBuilder(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            Services = services;
        }

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection"/> services are attached to.
        /// </value>
        public IServiceCollection Services { get; private set; }
    }
}
