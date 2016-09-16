// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Wangkanai.Browser.Resolvers;
using Xunit;

namespace Wangkanai.Browser.Test
{
    public class DeviceResolverTest
    {
        [Fact]
        public void setup()
        {
            //// arrange
            //var provider = new Mock<IServiceProvider>();            
            //var services = new Mock<IServiceCollection>();
            //services.Setup(x => x.BuildServiceProvider()).Returns(provider.Object);
            
            //// act
            //services.Object.AddClientService()
            //    .AddDevice();
            //// assert
            //var resolver = services.Object.BuildServiceProvider().GetRequiredService<IDeviceResolver>() as DeviceResolver;
            //Assert.NotNull(resolver);            
        }
    }
}