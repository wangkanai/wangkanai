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

            var accessor = new HttpContextAccessor {HttpContext = context};

            var useragentService = new UserAgentService(accessor);

            Assert.NotNull(useragentService.Context);
            Assert.NotNull(useragentService.UserAgent);
            Assert.Equal(userAgent, useragentService.UserAgent.ToString());
        }

        [Fact]
        public void Ctor_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAgentService(null));
        }

        [Fact]
        public void Ctor_HttpContextAccessorNotResolved_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAgentService(new HttpContextAccessor()));
        }

        [Fact]
        public void Ctor_HttpContextNull_ThrowsArgumentNullException()
        {
            var accessor = new HttpContextAccessor();
            
            Assert.Null(accessor.HttpContext);
            Assert.Throws<ArgumentNullException>(() => new UserAgentService(accessor));
        }

        private class ServiceProvider : IServiceProvider
        {
            public IHttpContextAccessor HttpContextAccessor { get; set; }

            public object GetService(Type serviceType)
                => HttpContextAccessor;
        }
    }
}