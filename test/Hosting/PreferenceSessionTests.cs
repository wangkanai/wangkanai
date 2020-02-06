// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class PreferenceSessionTests
    {
        [Fact]
        public async Task ReadingEmptySessionDoesNotCreateCookie()
        {
            var builder = new WebHostBuilder()
                          .ConfigureServices(services => { services.AddDetection(); })
                          .Configure(app =>
                          {
                              app.UseDetection();
                              app.Run(context =>
                              {
                                  context.Session.SetString("Key", "Value");
                                  return Task.FromResult(0);
                              });
                          });

            using var server = new TestServer(builder);

            var client   = server.CreateClient();
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            Assert.True(response.Headers.TryGetValues("Set-Cookie", out var values));
            Assert.Single(values);
        }

        // [Fact]
        // public async void ClientWantDesktopView()
        // {
        //     using var server = MockServer.CreateServer();
        //
        //     var client   = server.CreateClient();
        //     var request  = MockClient.CreateRequest(Device.Mobile);
        //     var response = await client.SendAsync(request);
        //     response.EnsureSuccessStatusCode();
        //     Assert.Contains("mobile", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
        //
        //     request  = MockClient.CreateRequest(Device.Mobile, "/detection/preference/prefer");
        //     response = await client.SendAsync(request);
        //     response.EnsureSuccessStatusCode();
        //     Assert.Contains("desktop", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
        // }
    }
}