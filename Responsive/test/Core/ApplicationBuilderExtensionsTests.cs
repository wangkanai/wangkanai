// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using System;
using Xunit;

namespace Wangkanai.Responsive.Test.Core
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseResponsive_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IApplicationBuilder)null).UseResponsive());
        }

        [Fact]
        public void UseResponsive_IApplicationBuilder_Null_ThrowsArgumentNullException()
        {
            var app = new ApplicationBuilder(ServiceProvider.Instance);
            Assert.Throws<ArgumentNullException>(() => app.UseResponsive(null));
        }

        private static class ServiceProvider
        {
            public static IServiceProvider Instance { get; set; }
        }
    }
}
