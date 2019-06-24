// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Wangkanai.Responsive.Test
{
    public partial class ResponsiveCollectionExtensionsTests
    {
        [Fact]
        public void AddResponsive_Services()
        {
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddResponsive();

            Assert.Equal(6, builder.Services.Count);
            Assert.Same(serviceCollection, builder.Services);
        }

        [Fact]
        public void AddResponsive_Null_ArgumentNullException()
        {
            Assert.Throws<AddResponsiveArgumentNullException>(() => ((IServiceCollection)null).AddResponsive());
        }
    }
}
