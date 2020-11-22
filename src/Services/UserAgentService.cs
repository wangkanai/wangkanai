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
        public UserAgent   UserAgent { get; }

        public UserAgentService(IHttpContextService context)
        {
            UserAgent = new UserAgent(context.Request.Headers["User-Agent"].FirstOrDefault() ?? "");
        }
    }
}