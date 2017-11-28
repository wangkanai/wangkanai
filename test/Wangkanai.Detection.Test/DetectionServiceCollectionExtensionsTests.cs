// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DetectionServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddDetection_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddDetection();
            var serviceDescriptors = new List<ServiceDescriptor>
            {
                new ServiceDescriptor(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton),
                new ServiceDescriptor(typeof(IDetectionService), typeof(DetectionService), ServiceLifetime.Transient)
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
