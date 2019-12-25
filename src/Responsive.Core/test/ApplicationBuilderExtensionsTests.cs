// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Xunit;

namespace Wangkanai.Responsive.Core
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseResponsive_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<UseResponsiveAppArgumentNullException>(() => ((IApplicationBuilder)null).UseResponsive());
        }

        private static class ServiceProvider
        {
            public static IServiceProvider Instance { get; set; }
        }
    }
}
