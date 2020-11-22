// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Mocks;
using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsiveMiddlewareTest
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
                async () => await middleware.InvokeAsync(null, null)
            );
        }

        [Fact]
        public async void Invoke_HttpContext_ResponsiveService_Null_ThrowsNullReferenceException()
        {
            var service    = MockService.HttpContext();
            var middleware = new ResponsiveMiddleware(Next);

            await Assert.ThrowsAsync<NullReferenceException>(
                async () => await middleware.InvokeAsync(service.Context, null)
            );
        }

        [Fact]
        public async void Invoke_HttpContext_ResponsiveService_Success()
        {
            using var server   = MockServer.Server();
            var       request  = MockClient.CreateRequest(Device.Desktop);
            var       client   = server.CreateClient();
            var       response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Contains(
                "desktop",
                await response.Content.ReadAsStringAsync(),
                StringComparison.OrdinalIgnoreCase);
        }
    }
}