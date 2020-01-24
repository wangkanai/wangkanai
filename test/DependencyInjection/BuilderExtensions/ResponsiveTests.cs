// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
            var service = new ServiceCollection();
            var expected = service.Count + total;
            var builder = service.AddDetection();

            //Assert.Equal(expected, builder.Services.Count);
            Assert.Same(service, builder.Services);
        }

        [Fact]
        public void AddResponsive_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(CreateResponsiveNullService);
        }

        [Fact]
        public void AddResponsive_Options_Builder_Service()
        {
            var service = new ServiceCollection();
            var expected = service.Count + total;
            var builder = service.AddDetection(options =>
                                               {
                                                   options.Responsive.DefaultTablet = Device.Desktop;
                                               });

            //Assert.Equal(expected, builder.Services.Count);
            Assert.Same(service, builder.Services);
        }

        [Fact]
        public void AddResponsive_Options_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(CreateResponsiveNullService);
        }
        
        private readonly Func<object> CreateResponsiveNullService =
            () => ((IServiceCollection)null).AddDetection();
        
        [Fact]
        public void AddResponsive_Options_Disable_True()
        {
            var services  = new ServiceCollection();
            services.AddLogging();
            services.AddDetection(options => options.Responsive.Disable = true);
            
            var provider = services.BuildServiceProvider();

            var app = new ApplicationBuilder(provider);
            
            app.UseDetection();
            
            var service = MockService.CreateService("mobile");
            var request = app.Build();
            request.Invoke(service.Context);
        }
        
        [Fact]
        public void AddResponsive_Options_Disable_False()
        {
            var services = new ServiceCollection();
            services.AddHttpContextAccessor();
            services.AddLogging();
            services.AddDetection();
            
            var provider = services.BuildServiceProvider();
            var mock = new Mock<IServiceProvider>();

            var app = new ApplicationBuilder(provider);
            
            app.UseDetection();

            var service = MockService.CreateService("mobile");
            var request = app.Build();
            request.Invoke(service.Context);
        }
        
        [Fact]
        public void AddResponsive_Options_Disable_False_Failed()
        {
            var services = new ServiceCollection();
            services.AddHttpContextAccessor();
            services.AddLogging();
            services.AddDetection();
            
            var provider = services.BuildServiceProvider();

            var app = new ApplicationBuilder(provider);
            
            app.UseDetection();

            var service = MockService.CreateService("mobile");
            var request = app.Build();
            request.Invoke(service.Context);
        }
    }
}
