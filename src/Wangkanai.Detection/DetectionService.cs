// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Wangkanai.Detection.Abstractions;

namespace Wangkanai.Detection
{
    /// <summary>
    /// Provides the APIs for query client access device.
    /// </summary>
    public class DetectionService : IDetectionService
    {
        public HttpContext Context { get; }
        public IUserAgent UserAgent { get; }

        public DetectionService(IServiceProvider services)
        {
            if (services != null) Context = services.GetService<IHttpContextAccessor>()?.HttpContext;

            UserAgent = CreateUserAgent(Context);
        }

        private IUserAgent CreateUserAgent(HttpContext context)
        {
            // fail in unit testing
            //if (Context == null) throw new ArgumentNullException(nameof(Context));  
            if (context != null) return new UserAgent(Context.Request.Headers["User-Agent"].FirstOrDefault());

            return new UserAgent();
        }
    }
}
