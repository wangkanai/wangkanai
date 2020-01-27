// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Moq;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    [DebuggerStepThrough]
    public static class MockService
    {
        public static PlatformService CreatePlatformService(string agent) 
            => new PlatformService(CreateService(agent));

        public static EngineService CreateEngineService(string agent)
        {
            var service  = CreateService(agent);
            var platform = new PlatformService(service);
            return new EngineService(service, platform);;
        }
        
        public static CrawlerService CreateCrawlerService(string agent, DetectionOptions options = null) 
            => new CrawlerService(CreateService(agent), options);

        public static DeviceService CreateDeviceService(string value, string header) 
            => new DeviceService(CreateService(value, header));

        public static DeviceService CreateDeviceService(string agent) 
            => new DeviceService(CreateService(agent));

        public static IUserAgentService CreateService(string agent)
            => MockUserAgentService(agent).Object;

        public static IUserAgentService CreateService(string value, string header)
            => MockUserAgentService(value, header).Object;

        #region internal
        private static HttpContext DefaultHttpContext()
            => new DefaultHttpContext();
        
        private static HttpContext CreateContext(string value)
            => CreateContext(value, "User-Agent");

        private static HttpContext CreateContext(string value, string header)
        {
            var context = DefaultHttpContext();
            context.Request.Headers.Add(header, new[] {value});
            return context;
        }
        
        private static Mock<IUserAgentService> MockUserAgentService(string value, string header)
            => CreateContext(value, header).SetupUserAgent(null);

        private static Mock<IUserAgentService> MockUserAgentService(string agent)
            => CreateContext(agent).SetupUserAgent(agent);

        private static Mock<IUserAgentService> SetupUserAgent(
            this HttpContext context, string agent)
        {
            var service = new Mock<IUserAgentService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent(agent));
            return service;
        }
        #endregion
    }
}
