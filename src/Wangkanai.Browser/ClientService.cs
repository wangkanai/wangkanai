// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{
    /// <summary>
    /// Provides the APIs for query client access device.
    /// </summary>
    public class ClientService : IClientService
    {
        public HttpContext Context { get; }
        public UserAgent UserAgent { get; }

        public ClientService(IServiceProvider services)
        {
            if (services != null) Context = services.GetService<IHttpContextAccessor>()?.HttpContext;

            UserAgent = CreateUserAgent(Context);
        }

        private UserAgent CreateUserAgent(HttpContext context)
        {
            // fail in unit testing
            //if (Context == null) throw new ArgumentNullException(nameof(Context));  
            if (context != null) return new UserAgent(Context.Request.Headers["User-Agent"].FirstOrDefault());

            return new UserAgent();
        }
    }
}
