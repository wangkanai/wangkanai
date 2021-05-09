// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class UserAgentService : IUserAgentService
    {
        public           UserAgent                 UserAgent { get; }

        public UserAgentService(IHttpContextService context, ILogger<UserAgentService> logger)
        {
            UserAgent    = new UserAgent(context.Request.Headers["User-Agent"].FirstOrDefault() ?? "");
            logger.LogInformation(Log(DateTime.Now, context.Context.Connection.RemoteIpAddress, UserAgent));
        }

        private string Log(DateTime now, IPAddress remote, UserAgent agent)
            => $"{now.ToUniversalTime()} UTC : IPv4 {remote} => {agent}";
    }
}