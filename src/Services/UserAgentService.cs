// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class UserAgentService : IUserAgentService
    {
        public HttpContext Context   { get; }
        public UserAgent   UserAgent { get; }

        public UserAgentService(IHttpContextAccessor accessor)
        {
            if (accessor is null)
                throw new ArgumentNullException(nameof(accessor));
            if (accessor.HttpContext is null)
                throw new ArgumentNullException(nameof(accessor.HttpContext));

            Context = accessor.HttpContext;

            var agent = Context.Request.Headers["User-Agent"].FirstOrDefault();

            UserAgent = new UserAgent(agent ?? "");
        }
    }
}