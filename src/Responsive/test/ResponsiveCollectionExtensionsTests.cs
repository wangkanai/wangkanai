// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Wangkanai.Responsive.Test
{
    public class ResponsiveCollectionExtensionsTests
    {
        [Fact]
        public void AddResponsive_Services()
        {
            var service = new ServiceCollection();
            var builder = service.AddResponsive();

            Assert.Equal(12, builder.Services.Count);
            Assert.Same(service, builder.Services);
        }

        [Fact]
        public void AddResponsive_Null_ArgumentNullException()
        {
            Func<object> CreateNullService = () =>
            {
                return ((IServiceCollection)null).AddResponsive();
            };

            Assert.Throws<AddResponsiveArgumentNullException>(CreateNullService);
        }

        [Fact]
        public void AddResponsive_Option_Builder_Service()
        {
            var service = new ServiceCollection();
            var builder = service.AddResponsive(options =>
            {
                options.View.DefaultTablet = Detection.DeviceType.Desktop;
            });

            Assert.Equal(12, builder.Services.Count);
            Assert.Same(service, builder.Services);
        }

        [Fact]
        public void AddResponsive_Options_Null_ArgumentNullException()
        {
            Func<object> CreateNullService = () =>
            {
                return ((IServiceCollection)null).AddResponsive(options => { });
            };

            Assert.Throws<AddResponsiveArgumentNullException>(CreateNullService);
        }
    }
}
