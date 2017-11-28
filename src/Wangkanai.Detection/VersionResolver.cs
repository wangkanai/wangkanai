// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public sealed class VersionResolver : IVersionResolver
    {
        /// <summary>
        /// Get version result of the implementation
        /// </summary>
        public IVersion Version => _version;
        /// <summary>
        /// Get user agnet of the request client
        /// </summary>
        public IUserAgent UserAgent => _service.UserAgent;

        private readonly IDetectionService _service;
        private readonly Version _version;

        public VersionResolver(IDetectionService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _service = service;
        }
    }
}