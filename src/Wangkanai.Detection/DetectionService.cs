// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            this.Context = services.GetRequiredService<IHttpContextAccessor>().HttpContext;
            this.UserAgent = CreateUserAgent(this.Context);
        }

        private IUserAgent CreateUserAgent(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(Context)); 
            
            return new UserAgent(Context.Request.Headers["User-Agent"].FirstOrDefault());
        }
    }
}
