// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Wangkanai.Responsive.Core
{
    public class ApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseResponsive_ThrowsInvalidOptionException_IfResponsiveMarkerServiceIsNotRegistered()
        {
            // Arrange
            var applicationBuilderMock = new Mock<IApplicationBuilder>();
            applicationBuilderMock
                .Setup(s => s.ApplicationServices)
                .Returns(Mock.Of<IServiceProvider>());

            // Act
            var exception = Assert.Throws<InvalidOperationException>(
                () => applicationBuilderMock.Object.UseResponsive());

            // Assert
            Assert.Equal("AddResponsive() is not added to ConfigureServices(...)", exception.Message);
        }

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
