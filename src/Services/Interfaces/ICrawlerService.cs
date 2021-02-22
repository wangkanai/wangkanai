// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services.Interfaces
{
    /// <summary>
    /// Provides the APIs for query <see cref="Crawler"/>.
    /// </summary>
    public interface ICrawlerService
    {
        /// <summary>
        /// Determine that the request client is crawler.
        /// </summary>
        public bool IsCrawler { get; }
        
        /// <summary>
        /// Gets the <see cref="Crawler"/> name of the request clients.
        /// </summary>
        public Crawler Name { get; }
        
        /// <summary>
        /// Gets the <see cref="Version"/> of the request client.
        /// </summary>
        public Version Version { get; }
    }
}
