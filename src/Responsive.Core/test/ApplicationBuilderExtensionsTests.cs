// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Responsive.Core
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseResponsive_Null_ThrowsArgumentNullException()
        {
            Func<object> testCode = () =>
            {
                return ((IApplicationBuilder)null).UseResponsive();
            };
            Assert.Throws<UseResponsiveAppArgumentNullException>(testCode);
        }

        private static class ServiceProvider
        {
            public static IServiceProvider Instance { get; set; }
        }
    }
}
