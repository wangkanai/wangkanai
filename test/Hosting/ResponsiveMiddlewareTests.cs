// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsiveMiddlewareTests
    {
        private static Task Next(HttpContext d)
            => Task.Factory.StartNew(() => d);

        [Fact]
        public void Ctor_RequestDelegate_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ResponsiveMiddleware(null)
            );
        }

        [Fact]
        public async void Invoke_HttpContext_Null_ResponsiveService_Null_ThrowsArgumentNullException()
        {
            var middleware = new ResponsiveMiddleware(Next);

            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await middleware.Invoke(null, null)
            );
        }

        [Fact]
        public async void Invoke_HttpContext_ResponsiveService_Null_ThrowsNullReferenceException()
        {
            var service    = MockService.CreateService(null);
            var middleware = new ResponsiveMiddleware(Next);

            await Assert.ThrowsAsync<NullReferenceException>(
                async () => await middleware.Invoke(service.Context, null)
            );
        }

        [Fact]
        public async void Invoke_HttpContext_ResponsiveService_Success()
        {
            var service    = MockService.CreateService(null);
            var options    = new DetectionOptions();
            var device     = new DeviceService(service);
            var preference = Mock.Of<IPreferenceService>();
            var resolver   = new ResponsiveService(device, preference, options);

            var middleware = new ResponsiveMiddleware(Next);

            await middleware.Invoke(service.Context, resolver);

            Assert.Equal(Device.Desktop, service.Context.GetDevice());
        }
    }
}