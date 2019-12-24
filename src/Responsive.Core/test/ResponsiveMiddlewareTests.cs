// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using Wangkanai.Detection;

using Xunit;

namespace Wangkanai.Responsive.Core
{
    public class ResponsiveMiddlewareTests
    {
        [Fact]
        public void Ctor_RequestDelegate_ResponsiveOptions_Success()
        {
            var options = Options.Create(new ResponsiveOptions());
            var middleware = new ResponsiveMiddleware(d => Task.Factory.StartNew(() => d), options);
        }

        [Fact]
        public void Ctor_Null_ResponsiveOptions_ThrowsArgumentNullException()
        {
            Assert.Throws<ResponsiveMiddlewareNextArgumentNullException>(() => new ResponsiveMiddleware(null, Options.Create(new ResponsiveOptions())));
        }

        [Fact]
        public void Ctor_RequestDelegate_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ResponsiveMiddlewareOptionArgumentNullException>(() => new ResponsiveMiddleware(d => Task.Factory.StartNew(() => d), null));
        }

        [Fact]
        public async void Invoke_HttpContext_IDeviceResolver_Success()
        {
            var context = new DefaultHttpContext();
            var options = Options.Create(new ResponsiveOptions());
            var middleware = new ResponsiveMiddleware(d => Task.Factory.StartNew(() => d), options);

            await middleware.Invoke(context, new DeviceResolver());

            Assert.Equal(DeviceType.Tablet, context.GetDevice());
        }

        [Fact]
        public async void Invoke_Null_IDeviceResolver_ThrowsArgumentNullException()
        {
            var options = Options.Create(new ResponsiveOptions());
            var middleware = new ResponsiveMiddleware(d => Task.Factory.StartNew(() => d), options);

            await Assert.ThrowsAsync<ResponsiveMiddlewareInvokeArgumentNullException>(async () => await middleware.Invoke(null, new DeviceResolver()));
        }

        [Fact]
        public async void Invoke_HttpContext_Null_ThrowsNullReferenceException()
        {
            var context = new DefaultHttpContext();
            var options = Options.Create(new ResponsiveOptions());
            var middleware = new ResponsiveMiddleware(d => Task.Factory.StartNew(() => d), options);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await middleware.Invoke(context, null));
        }

        private class DeviceResolver : IDeviceResolver
        {
            public IDevice Device => new MyTablet() { Type = DeviceType.Tablet };

            public IUserAgent UserAgent => throw new NotImplementedException();
        }

        private class MyTablet : IDevice
        {
            public DeviceType Type { get; set; }

            public bool Crawler { get; set; }
        }
    }
}
