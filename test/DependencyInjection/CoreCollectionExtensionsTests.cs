// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Services;

using Xunit;

namespace Wangkanai.Detection.DependencyInjection
{
    public class CoreCollectionExtensionsTests
    {
        [Fact]
        public void AddRequiredPlatformServices_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddDetectionBuilder().AddRequiredPlatformServices();
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
            var builder = serviceCollection.AddDetectionBuilder().AddCoreServices();
            var serviceDescriptors = new List<ServiceDescriptor>
            {
                new ServiceDescriptor(typeof(IUserAgentService), typeof(DefaultUserAgentService), ServiceLifetime.Transient),
                new ServiceDescriptor(typeof(IDeviceService), typeof(DefaultDeviceService), ServiceLifetime.Transient),
                new ServiceDescriptor(typeof(IEngineService), typeof(DefaultEngineService), ServiceLifetime.Transient),
                new ServiceDescriptor(typeof(IPlatformService), typeof(DefaultPlatformService), ServiceLifetime.Transient),
                new ServiceDescriptor(typeof(IBrowserService), typeof(DefaultBrowserService), ServiceLifetime.Transient),
                new ServiceDescriptor(typeof(ICrawlerService), typeof(DefaultCrawlerService), ServiceLifetime.Transient),
                new ServiceDescriptor(typeof(IDetectionService), typeof(DefaultDetectionService), ServiceLifetime.Transient),
            };

            Assert.NotNull(builder);
            Assert.NotNull(builder.Services);
            AssertServices(serviceDescriptors, builder.Services);
        }

        [Fact]
        public void AddDetection_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null).AddDetection());
        }

        [Fact]
        public void AddDetectionCore_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null).AddDetectionBuilder());
        }

        private void AssertServices(List<ServiceDescriptor> serviceDescriptors, IServiceCollection services)
        {
            for (int i = 0; i < serviceDescriptors.Count; i++)
            {
                Assert.Equal(serviceDescriptors[i].ServiceType, services[i].ServiceType);
                Assert.Equal(serviceDescriptors[i].ImplementationInstance, services[i].ImplementationInstance);
                Assert.Equal(serviceDescriptors[i].Lifetime, services[i].Lifetime);
            }
        }
    }
}
