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
        {
            var context = CreateContext(value, header);
            var service = new Mock<IUserAgentService>();
            service.SetupUserAgent(context, null);
            return service.Object;
        }

        private static Mock<IUserAgentService> MockUserAgentService(string agent)
        {
            var context = CreateContext(agent);
            var service = new Mock<IUserAgentService>();
            service.SetupUserAgent(context, agent);
            return service;
        }

        private static Mock<IUserAgentService> SetupUserAgent(this Mock<IUserAgentService> service,HttpContext context, string agent)
        {
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
