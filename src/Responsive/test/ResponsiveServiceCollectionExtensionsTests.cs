// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Wangkanai.Responsive.Test
{
    public partial class ResponsiveServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddResponsive_Services()
        {
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddResponsive();

            Assert.Equal(3, builder.Services.Count);
            Assert.Same(serviceCollection, builder.Services);
        }

        [Fact]
        public void AddResponsive_Null_ArgumentNullException()
        {
            Assert.Throws<AddResponsiveArgumentNullException>(() => ((IServiceCollection)null).AddResponsive());
        }
    }
}
