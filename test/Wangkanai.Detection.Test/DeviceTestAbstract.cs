// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Moq;

namespace Wangkanai.Detection.Test
{
    public abstract class DeviceTestAbstract
    {
        private HttpRequest CreateRequest() => new DefaultHttpContext().Request;
        private HttpContext CreateContext() => new DefaultHttpContext();
        protected IClientService CreateService(string agent)
        {
            var context = CreateContext(agent);
            var service = new Mock<IClientService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent(agent));
            return service.Object;
        }

        protected IClientService CreateService(string header, string value)
        {
            var context = CreateContext(header, value);
            var service = new Mock<IClientService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent());
            return service.Object;
        }
        protected HttpContext CreateContext(string value)
        {
            var context = CreateContext();
            var header = "User-Agent";
            context.Request.Headers.Add(header, new[] {value});
            return context;
        }

        protected HttpContext CreateContext(string header, string value)
        {
            var context = CreateContext();
            context.Request.Headers.Add(header, new[] {value});
            return context;
        }
        protected HttpRequest CreateRequest(string value)
        {
            var request = CreateRequest();
            var header = "User-Agent";
            request.Headers.Add(header, new[] { value });

            return request;
        }

        protected HttpRequest CreateRequest(string header, string value)
        {
            var request = CreateRequest();
            request.Headers.Add(header, new[] { value });

            return request;
        }

    }
}