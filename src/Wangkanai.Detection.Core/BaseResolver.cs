// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Wangkanai.Detection
{
    public class BaseResolver : IResolver
    {
        /// <summary>
        /// Get user agnet of the request client
        /// </summary>
        public IUserAgent UserAgent => _service.UserAgent;
        /// <summary>
        /// Get HttpContext of the application service
        /// </summary>
        protected HttpContext Context => _service.Context;

        protected readonly IUserAgentService _service;

        public BaseResolver(IUserAgentService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _service = service;
        }

        protected string GetUserAgent()
        {
            if (Context == null) return "";
            if (!Context.Request.Headers["User-Agent"].Any()) return "";
            return new UserAgent(Context.Request.Headers["User-Agent"].FirstOrDefault()).ToString().ToLowerInvariant();
        }
    }
}
