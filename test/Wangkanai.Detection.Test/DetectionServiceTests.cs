
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DetectionServiceTests
    {
        [Fact]
        public void Ctor_IServiceProvider_Success()
        {
            string userAgent = "Agent";
            var context = new DefaultHttpContext();
            context.Request.Headers["User-Agent"] = userAgent;
            var serviceProvider = new ServiceProvider()
            {
                HttpContextAccessor = new HttpContextAccessor()
                {
                    HttpContext = context
                }
            };

            var detectionService = new DetectionService(serviceProvider);

            Assert.NotNull(detectionService.Context);
            Assert.NotNull(detectionService.UserAgent);
            Assert.Equal(userAgent.ToLowerInvariant(), detectionService.UserAgent.ToString());
        }

        [Fact]
        public void Ctor_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DetectionService(null));
        }

        [Fact]
        public void Ctor_HttpContextAccessorNotResolved_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new DetectionService(new ServiceProvider()));
        }

        [Fact]
        public void Ctor_HttpContextNull_ThrowsArgumentNullException()
        {
            var serviceProvider = new ServiceProvider()
            {
                HttpContextAccessor = new HttpContextAccessor()
            };

            Assert.Null(serviceProvider.HttpContextAccessor.HttpContext);
            Assert.Throws<ArgumentNullException>(() => new DetectionService(serviceProvider));
        }

        private class ServiceProvider : IServiceProvider
        {
            public IHttpContextAccessor HttpContextAccessor { get; set; }

            public object GetService(Type serviceType)
            {
                return this.HttpContextAccessor;
            }
        }
    }
}
