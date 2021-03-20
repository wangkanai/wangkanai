// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services.Interfaces
{
    /// <summary>
    /// Provides the APIs for query client <see cref="Browser"/>.
    /// </summary>
    public interface IBrowserService
    {
        /// <summary>
        /// Gets the <see cref="Browser"/> name of the request client.
        /// </summary>
        public Browser Name { get; }
        
        /// <summary>
        /// Gets the <see cref="Version"/> of the request client. 
        /// </summary>
        public Version Version { get; }
    }
}
