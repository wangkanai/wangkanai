// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    /// <summary>
    /// Provides the APIs for query client detection services.
    /// </summary>
    public interface IDetectionService
    {
        /// <summary>
        /// Get the <see cref="UserAgent"/> of the request client.
        /// </summary>
        public UserAgent UserAgent { get; }
        
        /// <summary>
        /// Get the <see cref="Device"/> resolved of the request client. 
        /// </summary>
        public IDeviceService Device { get; }

        /// <summary>
        /// Get the <see cref="Platform"/> resolved of the request client. 
        /// </summary>
        public IPlatformService Platform { get; }
        
        /// <summary>
        /// Get the <see cref="Engine"/> resolved of the request client. 
        /// </summary>
        public IEngineService Engine { get; }
        
        /// <summary>
        /// Get the <see cref="Browser"/> resolved of the request client. 
        /// </summary>
        public IBrowserService Browser { get; }

        /// <summary>
        /// Get the <see cref="Crawler"/> resolved of the request client. 
        /// </summary>
        public ICrawlerService Crawler { get; }
    }
}
