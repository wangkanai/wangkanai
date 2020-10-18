// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wangkanai.Detection.Services;
using Xunit;

namespace Wangkanai.Detection.DependencyInjection
{
    public class CoreBuilderExtensionsTest
    {
        [Fact]
        public void AddRequiredPlatformServices_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder           = serviceCollection.AddDetectionBuilder().AddRequiredPlatformServices();
            var serviceDescriptors = new List<ServiceDescriptor>
            {
                new ServiceDescriptor(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton),
                new ServiceDescriptor(typeof(IOptions<>), typeof(DetectionOptions), ServiceLifetime.Singleton),
                new ServiceDescriptor(typeof(IOptionsSnapshot<>), typeof(DetectionOptions), ServiceLifetime.Scoped),
                new ServiceDescriptor(typeof(IOptionsMonitor<>), typeof(DetectionOptions), ServiceLifetime.Singleton),
                new ServiceDescriptor(typeof(IOptionsFactory<>), typeof(DetectionOptions), ServiceLifetime.Transient),
                new ServiceDescriptor(typeof(IOptionsMonitorCache<>), typeof(DetectionOptions), ServiceLifetime.Singleton),
                new ServiceDescriptor(typeof(DetectionOptions), typeof(DetectionOptions), ServiceLifetime.Singleton)
            };

            Assert.NotNull(builder);
            Assert.NotNull(builder.Services);
            AssertServices(serviceDescriptors, builder.Services);
        }

        [Fact]
        public void AddCoreServices_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder           = serviceCollection.AddDetectionBuilder().AddCoreServices();
            var serviceDescriptors = new List<ServiceDescriptor>
            {
                new ServiceDescriptor(typeof(IUserAgentService), typeof(UserAgentService), ServiceLifetime.Scoped),
                new ServiceDescriptor(typeof(IDeviceService), typeof(DeviceService), ServiceLifetime.Scoped),
                new ServiceDescriptor(typeof(IEngineService), typeof(EngineService), ServiceLifetime.Scoped),
                new ServiceDescriptor(typeof(IPlatformService), typeof(PlatformService), ServiceLifetime.Scoped),
                new ServiceDescriptor(typeof(IBrowserService), typeof(BrowserService), ServiceLifetime.Scoped),
                new ServiceDescriptor(typeof(ICrawlerService), typeof(CrawlerService), ServiceLifetime.Scoped),
                new ServiceDescriptor(typeof(IDetectionService), typeof(DetectionService), ServiceLifetime.Scoped),
            };

            Assert.NotNull(builder);
            Assert.NotNull(builder.Services);
            AssertServices(serviceDescriptors, builder.Services);
        }

        [Fact]
        public void AddMarkerServices_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder           = serviceCollection.AddDetectionBuilder().AddMarkerService();
            var serviceDescriptors = new List<ServiceDescriptor>
            {
                new ServiceDescriptor(typeof(DetectionMarkerService), typeof(DetectionMarkerService), ServiceLifetime.Singleton),
            };

            Assert.NotNull(builder);
            Assert.NotNull(builder.Services);
            AssertServices(serviceDescriptors, builder.Services);
        }

        [Fact]
        public void AddDetection_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IServiceCollection) null).AddDetection());
        }

        [Fact]
        public void AddDetectionBuilder_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IServiceCollection) null).AddDetectionBuilder());
        }

        private void AssertServices(List<ServiceDescriptor> serviceDescriptors, IServiceCollection services)
        {
            for (var i = 0; i < serviceDescriptors.Count; i++)
            {
                Assert.Equal(serviceDescriptors[i].ServiceType, services[i].ServiceType);
                Assert.Equal(serviceDescriptors[i].ImplementationInstance, services[i].ImplementationInstance);
                Assert.Equal(serviceDescriptors[i].Lifetime, services[i].Lifetime);
            }
        }
    }
}