// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class UserAgentService : IUserAgentService
    {
        public HttpContext Context { get; }
        public UserAgent UserAgent { get; }

        public UserAgentService(IServiceProvider services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));
            var context = services.GetRequiredService<IHttpContextAccessor>().HttpContext;
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            Context = context;
            UserAgent = Context.UserAgentFromHeader();
        }
    }
}
