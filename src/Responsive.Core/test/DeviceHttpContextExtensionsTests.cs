// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using Wangkanai.Detection;
using Xunit;


namespace Wangkanai.Responsive.Test.Core
{
    public class DeviceHttpContextExtensionsTests
    {
        [Fact]
        public void SetDevice_HttpContext_UserPerference_ReturnsExpected()
        {
            var context = new DefaultHttpContext();
            var preference = new UserPerference();
            string responsiveContextKey = "Responsive"; // May be we can make this constant public for testing purposes.

            context.SetDevice(preference);

            Assert.True(context.Items.ContainsKey(responsiveContextKey));
            Assert.Same(preference, context.Items[responsiveContextKey]);
        }

        [Fact]
        public void SetDevice_Null_UserPerference_ThrowsArgumentNullException()
        {
            Assert.Throws<SetDeviceArgumentNullException>(() => ((HttpContext)null).SetDevice(new UserPerference()));
        }

        [Fact]
        public void SetDevice_HttpContext_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<SetDeviceArgumentNullException>(() => new DefaultHttpContext().SetDevice(null));
        }

        [Fact]
        public void GetDevice_HttpContext_ReturnsExpected()
        {
            string device = DeviceType.Tablet.ToString();
            var preference = new UserPerference() { Resolver = device };
            var context = new DefaultHttpContext();
            context.SetDevice(preference);

            Assert.Same(preference, context.GetDevice());
            Assert.Same(preference.Resolver, context.GetDevice().Resolver);
        }

        [Fact]
        public void GetDevice_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<GetDeviceArgumentNullException>(() => ((HttpContext)null).GetDevice());
        }

        [Fact]
        public void SetDevice_InvalidContext_ReturnsNotNull()
        {
            var context = new DefaultHttpContext();

            Assert.NotNull(context.GetDevice());
            Assert.Equal("Desktop", context.GetDevice().Resolver);
        }
    }
}
