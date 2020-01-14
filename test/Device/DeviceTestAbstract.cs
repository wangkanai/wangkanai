// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

using Moq;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Test
{
    public abstract class DeviceTestAbstract
    {
        private HttpContext CreateContext() => new DefaultHttpContext();

        protected IUserAgentService CreateService(string agent)
        {
            var context = CreateContext(agent);
            var service = new Mock<IUserAgentService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent(agent));
            return service.Object;
        }

        protected IUserAgentService CreateService(string header, string value)
        {
            var context = CreateContext(header, value);
            var service = new Mock<IUserAgentService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent());
            return service.Object;
        }

        protected HttpContext CreateContext(string value)
        {
            var context = CreateContext();
            var header = "User-Agent";
            context.Request.Headers.Add(header, new[] { value });
            return context;
        }

        protected HttpContext CreateContext(string header, string value)
        {
            var context = CreateContext();
            context.Request.Headers.Add(header, new[] { value });
            return context;
        }
    }
}
