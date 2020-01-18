// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    /// <summary>
    /// Provides the APIs for query client access device.
    /// </summary>
    public class DefaultUserAgentService : IUserAgentService
    {
        /// <summary>
        /// Get HttpContext of the application service
        /// </summary>
        public HttpContext Context { get; }

        /// <summary>
        /// Get user agnet of the request client
        /// </summary>
        public UserAgent UserAgent { get; }

        public DefaultUserAgentService(IServiceProvider services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));
            var context = services.GetRequiredService<IHttpContextAccessor>().HttpContext;
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Context = context;
            UserAgent = CreateUserAgentFromContext(Context);
        }

        private static UserAgent CreateUserAgentFromContext(HttpContext context)
            => new UserAgent(context.Request.Headers["User-Agent"].FirstOrDefault());
    }
}
