// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsiveCollectionExtensionsTests
    {
        private readonly int total = 17;

        [Fact]
        public void AddResponsive_Services()
        {
            var service  = new ServiceCollection();
            var expected = service.Count + total;
            var builder  = service.AddDetection();

            Assert.Same(service, builder.Services);
        }

        private readonly Func<object> CreateResponsiveNullService = () => ((IServiceCollection) null).AddDetection();

        [Fact]
        public void AddResponsive_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(CreateResponsiveNullService);
        }

        [Fact]
        public void AddResponsive_Options_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(CreateResponsiveNullService);
        }

        [Fact]
        public void AddResponsive_Options_Builder_Service()
        {
            var service  = new ServiceCollection();
            var expected = service.Count + total;
            var builder  = service.AddDetection(options => { options.Responsive.DefaultTablet = Device.Desktop; });

            Assert.Same(service, builder.Services);
        }

        [Fact]
        public async void AddResponsive_Options_Disable_True()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                    services.AddDetection(options => { options.Responsive.Disable = true; })
                )
                .Configure(app =>
                {
                    app.UseDetection();
                    app.Run(context => context.Response.WriteAsync("0"));
                });

            using var server = new TestServer(builder);
            var client  = server.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            request.SetUserAgent("mobile");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal("0", await response.Content.ReadAsStringAsync());
        }
        
        [Fact]
        public async void AddResponsive_Options_Disable_False()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                    services.AddDetection(options => { options.Responsive.Disable = false; })
                )
                .Configure(app =>
                {
                    app.UseDetection();
                    app.Run(context => context.Response.WriteAsync("0"));
                });

            using var server  = new TestServer(builder);
            var request = server.CreateRequest("/");
            var response = await request.GetAsync();
        }

        [Fact]
        public async void AddResponsive_Options_Disable_True_Old()
        {
            var service  = MockService.CreateService("mobile");
            var options  = new DetectionOptions {Responsive = {Disable = true}};
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var request = app.Build();

            Assert.NotEqual(service.Context, new DefaultHttpContext());

            await request.Invoke(service.Context);

            Assert.Equal(Device.Desktop, service.Context.GetDevice());
        }

        [Fact]
        public async void AddResponsive_Options_Disable_False_Old()
        {
            var service  = MockService.CreateService("mobile");
            var options  = new DetectionOptions {Responsive = {Disable = false}};
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var request = app.Build();

            Assert.NotEqual(service.Context, new DefaultHttpContext());

            await request.Invoke(service.Context);

            Assert.Equal(Device.Mobile, service.Context.GetDevice());
        }

        [Theory]
        [InlineData(Device.Mobile, "desktop", "/api/dog")]
        [InlineData(Device.Mobile, "mobile", "/api/dog")]
        public async void AddResponsive_WebApi_Exclude_Api(Device device, string agent, string path)
        {
            var service = MockService.CreateService(agent);
            service.Context.Request.Path = path;
            var options = new DetectionOptions
                          {
                              Responsive =
                              {
                                  DefaultMobile  = device,
                                  DefaultTablet  = device,
                                  DefaultDesktop = device,
                                  IncludeWebApi  = false
                              }
                          };
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var requestDelegate = app.Build();

            Assert.Equal(path, service.Context.Request.Path);
            await requestDelegate.Invoke(service.Context);
            Assert.Equal(Device.Desktop, service.Context.GetDevice());
        }

        [Theory]
        [InlineData(Device.Desktop, "mobile", "")]
        [InlineData(Device.Desktop, "desktop", "")]
        [InlineData(Device.Desktop, "mobile", "/api/dog")]
        [InlineData(Device.Desktop, "desktop", "/api/dog")]
        [InlineData(Device.Mobile, "desktop", "")]
        [InlineData(Device.Mobile, "mobile", "")]
        public async void AddResponsive_WebApi_Exclude_NonApi(Device device, string agent, string path)
        {
            var service = MockService.CreateService(agent);
            service.Context.Request.Path = path;
            var options = new DetectionOptions
                          {
                              Responsive =
                              {
                                  DefaultMobile  = device,
                                  DefaultTablet  = device,
                                  DefaultDesktop = device,
                                  IncludeWebApi  = false
                              }
                          };
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var requestDelegate = app.Build();

            Assert.Equal(path, service.Context.Request.Path);
            await requestDelegate.Invoke(service.Context);
            Assert.Equal(device, service.Context.GetDevice());
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
        public async void AddResponsive_WebApi_Include_Api(Device device, string agent, string path)
        {
            var service = MockService.CreateService(agent);
            service.Context.Request.Path = path;
            service.Context.SetMark(true);
            var options = new DetectionOptions
                          {
                              Responsive =
                              {
                                  DefaultMobile  = device,
                                  DefaultTablet  = device,
                                  DefaultDesktop = device,
                                  IncludeWebApi  = true
                              }
                          };
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var requestDelegate = app.Build();

            Assert.Equal(path, service.Context.Request.Path);
            await requestDelegate.Invoke(service.Context);
            Assert.Equal(device, service.Context.GetDevice());
        }

        private static ResponsiveService MockResponsiveService(IUserAgentService service, DetectionOptions options)
        {
            var device     = new DeviceService(service);
            var preference = Mock.Of<IPreferenceService>();
            var resolver   = new ResponsiveService(device, preference, options);
            return resolver;
        }

        private static ApplicationBuilder MockApplicationBuilder(DetectionOptions options, ResponsiveService resolver)
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(s => s.GetService(typeof(ILoggerFactory)))
                .Returns(Mock.Of<NullLoggerFactory>());
            serviceProvider
                .Setup(s => s.GetService(typeof(DetectionOptions)))
                .Returns(options);
            serviceProvider
                .Setup(s => s.GetService(typeof(IResponsiveService)))
                .Returns(resolver);
            serviceProvider
                .Setup(s => s.GetService(typeof(MarkerService)))
                .Returns(new MarkerService());

            return new ApplicationBuilder(serviceProvider.Object);
        }
    }
}