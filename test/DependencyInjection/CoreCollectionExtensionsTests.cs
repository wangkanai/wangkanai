// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Detection.Services;

using Xunit;

namespace Wangkanai.Detection.DependencyInjection
{
    public class DetectionCoreCollectionExtensionsTests
    {
        [Fact]
        public void AddDetection_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddCoreServices();
            var serviceDescriptors = new List<ServiceDescriptor>
            {
                new ServiceDescriptor(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton),
                new ServiceDescriptor(typeof(IUserAgentService), typeof(UserAgentService), ServiceLifetime.Transient)
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
            Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null).AddCoreServices());
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
