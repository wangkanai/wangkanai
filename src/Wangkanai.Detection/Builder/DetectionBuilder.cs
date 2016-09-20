// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Detection.Abstractions;
using Wangkanai.Detection.Builder;

namespace Wangkanai.Detection.Builder
{
    /// <summary>
    /// Helper functions for configuring browser services.
    /// </summary>
    public class DetectionBuilder : IDetectionBuilder
    {
        /// <summary>
        /// Creates a new instance of <see cref="DetectionBuilder"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
        public DetectionBuilder(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.TryAddTransient<IClientInfo, ClientInfo>();

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
