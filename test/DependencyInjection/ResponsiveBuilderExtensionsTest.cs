// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Detection.Mocks;
using Wangkanai.Detection.Models;

using Xunit;

namespace Wangkanai.Detection.DependencyInjection
{
    public class ResponsiveBuilderExtensionsTest
    {
        [Fact]
        public void AddDetection_Services()
        {
            var service = new ServiceCollection();
            var builder = service.AddDetection();

            Assert.Same(service, builder.Services);
        }

        [Fact]
        public void AddDetection_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => ((IServiceCollection)null!).AddDetection());
        }

        [Fact]
        public void AddDetection_Options_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => ((IServiceCollection)null!).AddDetection(null!));
        }

        [Fact]
        public void AddDetection_Options_Builder_Service()
        {
            var service = new ServiceCollection();
            var builder = service.AddResponsive(
                options => { options.DefaultTablet = Device.Desktop; });

            Assert.Same(service, builder.Services);
        }

        [Fact]
        public async void AddDetection_Options_Disable_True()
        {
            using var server = MockServer.Server(options => { options.Disable = true; });

            var client   = server.CreateClient();
            var request  = MockClient.CreateRequest(Device.Mobile);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Contains("desktop", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
        }


        [Fact]
        public async void AddDetection_Options_Disable_False()
        {
            using var server = MockServer.Server(options => { options.Disable = false; });

            var client   = server.CreateClient();
            var request  = MockClient.CreateRequest(Device.Mobile);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Contains("mobile", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AddDetection_ResponsiveOptions_Disable_IncludeWebApi()
        {
            var builder = MockServer.WebHostBuilder(options =>
            {
                options.Disable       = true;
                options.IncludeWebApi = true;
            });

            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                using var server = new TestServer(builder);
            });
            Assert.Equal("IncludeWebApi is not needed if already Disable", exception.Message);
        }

        [Theory]
        [InlineData(Device.Mobile, "desktop", "/api/dog")]
        [InlineData(Device.Mobile, "mobile", "/api/dog")]
        public async void AddDetection_WebApi_Exclude_Api(Device device, string agent, string path)
        {
            using var server = MockServer.Server(options =>
            {
                options.DefaultMobile  = device;
                options.DefaultTablet  = device;
                options.DefaultDesktop = device;
                options.IncludeWebApi  = false;
            });

            var client   = server.CreateClient();
            var request  = MockClient.CreateRequest(agent, path);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Contains("desktop", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData(Device.Desktop, "mobile", "")]
        [InlineData(Device.Desktop, "desktop", "")]
        [InlineData(Device.Desktop, "mobile", "/api/dog")]
        [InlineData(Device.Desktop, "desktop", "/api/dog")]
        [InlineData(Device.Mobile, "desktop", "")]
        [InlineData(Device.Mobile, "mobile", "")]
        public async void AddDetection_WebApi_Exclude_NonApi(Device device, string agent, string path)
        {
            using var server = MockServer.Server(options =>
            {
                options.DefaultMobile  = device;
                options.DefaultTablet  = device;
                options.DefaultDesktop = device;
                options.IncludeWebApi  = false;
            });
            await server.Host.StartAsync();

            var client   = server.CreateClient();
            var request  = MockClient.CreateRequest(agent, path);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Contains(device.ToString(), await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData(Device.Desktop, "mobile", "")]
        [InlineData(Device.Desktop, "mobile", "/api/dog")]
        [InlineData(Device.Desktop, "desktop", "")]
        [InlineData(Device.Desktop, "desktop", "/api/dog")]
        [InlineData(Device.Mobile, "desktop", "")]
        [InlineData(Device.Mobile, "desktop", "/api/dog")]
        [InlineData(Device.Mobile, "mobile", "")]
        [InlineData(Device.Mobile, "mobile", "/api/dog")]
        public async void AddDetection_WebApi_Include_Api(Device device, string agent, string path)
        {
            using var server = MockServer.Server(options =>
            {
                options.DefaultMobile  = device;
                options.DefaultTablet  = device;
                options.DefaultDesktop = device;
                options.IncludeWebApi  = true;
            });

            var client   = server.CreateClient();
            var request  = MockClient.CreateRequest(agent, path);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Contains(device.ToString(), await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
        }
    }
}