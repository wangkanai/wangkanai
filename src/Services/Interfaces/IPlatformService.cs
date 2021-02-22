// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services.Interfaces
{
    /// <summary>
    /// Provides the APIs for query client platform.
    /// </summary>
    public interface IPlatformService
    {
        /// <summary>
        /// Gets the <see cref="Processor"/> of the request client.
        /// </summary>
        public Processor Processor { get; }
        
        /// <summary>
        /// Gets the <see cref="Platform"/> of the request client.
        /// </summary>
        public Platform Name { get; }
        
        /// <summary>
        /// Gets the <see cref="Version"/> of the request client. 
        /// </summary>
        public Version Version { get; }
    }
}
