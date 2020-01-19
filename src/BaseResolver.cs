// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public class BaseResolver : IResolver
    {
        /// <summary>
        /// Get user agent of the request client
        /// </summary>
        public UserAgent UserAgent => _service.UserAgent;

        /// <summary>
        /// Get HttpContext of the application service
        /// </summary>
        protected HttpContext Context => _service.Context;

        protected readonly IUserAgentService _service;

        protected BaseResolver(IUserAgentService service)
            => _service = service ?? throw new ArgumentNullException(nameof(service));
    }
}
