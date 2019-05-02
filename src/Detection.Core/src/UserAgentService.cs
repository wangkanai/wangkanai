// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Detection
{
    /// <summary>
    /// Provides the APIs for query client access device.
    /// </summary>
    public class UserAgentService : IUserAgentService
    {
        public HttpContext Context { get; }
        public IUserAgent UserAgent { get; }

        public UserAgentService(IServiceProvider services)
        {
            if (services == null) throw new UserAgentServiceArgumentNullException(nameof(services));

            this.Context = services.GetRequiredService<IHttpContextAccessor>().HttpContext;
            this.UserAgent = CreateUserAgent(this.Context);
        }

        public UserAgentService(HttpContext context)
        {
            if (context == null) throw new UserAgentServiceArgumentNullException(nameof(context));

            this.Context = context;
            this.UserAgent = CreateUserAgent(this.Context);
        }

        private IUserAgent CreateUserAgent(HttpContext context)
        {
            if (context == null) throw new UserAgentServiceArgumentNullException(nameof(Context));

            var agent = Context.Request.Headers["User-Agent"].FirstOrDefault();

            if (agent == null) return new UserAgent();

            return new UserAgent(agent);
        }
    }
}
