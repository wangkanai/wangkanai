// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// Modifications Copyright (c) 2020 Kapok Marketing, Inc.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services
{
    public class HttpContextService : IHttpContextService
    {
        public HttpContext Context { get; }
        public HttpRequest Request => Context.Request;

        public HttpContextService(IHttpContextAccessor accessor) 
            => Context = accessor?.HttpContext 
                         ?? new DefaultHttpContext();

        public static HttpContextService CreateService()
            => new HttpContextService(null!);
    }
}