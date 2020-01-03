// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection
{
    public class BaseResolver : IResolver
    {
        /// <summary>
        /// Get user agnet of the request client
        /// </summary>
        public UserAgent UserAgent => _service.UserAgent;

        /// <summary>
        /// Get HttpContext of the application service
        /// </summary>
        protected HttpContext Context => _service.Context;
        protected readonly IUserAgentService _service;

        public BaseResolver(IUserAgentService service)
            => _service = service ?? throw new ArgumentNullException(nameof(service));

        protected string GetUserAgent()
        {
            if (Context == null || !Context.Request.Headers["User-Agent"].Any())
                return "";

            return new UserAgent(Context.Request.Headers["User-Agent"].FirstOrDefault())
                .ToString()
                .ToLowerInvariant();
        }
    }
}
