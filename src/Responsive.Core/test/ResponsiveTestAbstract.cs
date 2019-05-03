// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.ComponentModel.Design;
using Wangkanai.Detection;

namespace Wangkanai.Responsive.Test
{
    public class ResponsiveTestAbstract
    {
        private HttpContext CreateContext() => new DefaultHttpContext();
        public virtual IResponsiveService CreateService(string agent)
        {
            var context = CreateContext(agent);
            var service = new Mock<IResponsiveService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent(agent));
            return service.Object;
        }
        public virtual HttpContext CreateContext(string value)
        {
            var context = CreateContext();
            var header = "User-Agent";
            context.Request.Headers.Add(header, new[] { value });
            return context;
        }
    }
}
