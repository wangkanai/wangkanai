// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{
    /// <summary>
    /// Helper functions for configuring browser services.
    /// </summary>
    public class ClientBuilder : IClientBuilder
    {
        /// <summary>
        /// Creates a new instance of <see cref="IClientBuilder"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
        public ClientBuilder(IServiceCollection services)
        {
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
