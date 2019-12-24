// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;

using Xunit;

namespace Wangkanai.Responsive.Core
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseResponsive_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<UseResponsiveArgumentNullException>(() => ((IApplicationBuilder)null).UseResponsive());
        }

        //[Fact]
        //public void UseResponsive_IApplicationBuilder_Null_ThrowsArgumentNullException()
        //{
        //    var app = new ApplicationBuilder(ServiceProvider.Instance);
        //    Assert.Throws<UseResponsiveOptionArgumentNullException>(() => app.UseResponsive(null));
        //}

        private static class ServiceProvider
        {
            public static IServiceProvider Instance { get; set; }
        }
    }
}
