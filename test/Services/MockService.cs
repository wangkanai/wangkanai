// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Moq;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public static class MockService
    {
        private static HttpContext DefaultHttpContext()
            => new DefaultHttpContext();

        public static IUserAgentService CreateService(string agent)
            => MockUserAgentService(agent).Object;
        public static IUserAgentService CreateService(string value, string header)
            => MockUserAgentService(value, header).Object;
        private static Mock<IUserAgentService> MockUserAgentService(string value, string header)
            => CreateContext(value, header).SetupUserAgent(null);
        private static Mock<IUserAgentService> MockUserAgentService(string agent)
            => CreateContext(agent).SetupUserAgent(agent);

        private static Mock<IUserAgentService> SetupUserAgent(
            this HttpContext context, string agent)
        {
            var service = new Mock<IUserAgentService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent(agent));
            return service;
        }

        public static HttpContext CreateContext(string value)
            => CreateContext(value, "User-Agent");

        public static HttpContext CreateContext(string value, string header)
        {
            var context = DefaultHttpContext();
            context.Request.Headers.Add(header, new[] { value });
            return context;
        }
    }
}
