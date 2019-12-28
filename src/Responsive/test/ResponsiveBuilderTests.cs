// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Responsive.Test.Core
{
    public class ResponsiveBuilderTests
    {
        [Fact]
        public void Ctor_Services_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder = new ResponsiveBuilder(serviceCollection);

            Assert.Same(serviceCollection, builder.Services);
        }

        [Fact]
        public void Ctor_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ResponsiveBuilderArgumentNullException>(() => new ResponsiveBuilder(null));
        }

        [Fact]
        public void Services_ReturnsExpected()
        {
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddResponsive();

            Assert.NotNull(builder.Services);
            Assert.NotEmpty(builder.Services);
        }
    }
}
