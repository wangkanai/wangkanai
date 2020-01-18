// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Detection Core builder Interface
    /// </summary>
    [Obsolete]
    public interface IDetectionCoreBuilder
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }
    }
}
