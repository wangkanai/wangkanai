// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class UserAgentServiceTests
    {
        [Fact]
        public void Ctor_IServiceProvider_Success()
        {
            string userAgent = "Agent";
            var    context   = new DefaultHttpContext();
            context.Request.Headers["User-Agent"] = userAgent;
            var serviceProvider = new ServiceProvider()
            {
                HttpContextAccessor = new HttpContextAccessor()
                {
                    HttpContext = context
                }
            };

            var useragentService = new UserAgentService(serviceProvider);

            Assert.NotNull(useragentService.Context);
            Assert.NotNull(useragentService.UserAgent);
            Assert.Equal(userAgent, useragentService.UserAgent.ToString());
        }

        [Fact]
        public void Ctor_Null_ThrowsArgumentNullException()
        {
            //Assert.Throws<ArgumentNullException>(() => new DefaultUserAgentService(null));
        }

        [Fact]
        public void Ctor_HttpContextAccessorNotResolved_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new UserAgentService(new ServiceProvider()));
        }

        [Fact]
        public void Ctor_HttpContextNull_ThrowsArgumentNullException()
        {
            var serviceProvider = new ServiceProvider()
            {
                HttpContextAccessor = new HttpContextAccessor()
            };

            Assert.Null(serviceProvider.HttpContextAccessor.HttpContext);
            Assert.Throws<ArgumentNullException>(() => new UserAgentService(serviceProvider));
        }

        private class ServiceProvider : IServiceProvider
        {
            public IHttpContextAccessor HttpContextAccessor { get; set; }

            public object GetService(Type serviceType)
                => HttpContextAccessor;
        }
    }
}
