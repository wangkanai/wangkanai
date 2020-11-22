// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Diagnostics;

using Microsoft.AspNetCore.Http;

using Moq;

using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Mocks
{
    [DebuggerStepThrough]
    public static class MockService
    {
        #region Responsive

        public static ResponsiveService Responsive(string agent, DetectionOptions options = null)
        {
            var accessor = HttpContextAccessor(agent);
            var device   = Device(agent);
            return Responsive(accessor, device, options);
        }

        private static ResponsiveService Responsive(IHttpContextAccessor accessor, IDeviceService device, DetectionOptions options = null)
            => new ResponsiveService(accessor, device, options);

        #endregion

        #region Browser

        public static BrowserService Browser(string agent)
            => Browser(UserAgent(agent));

        private static BrowserService Browser(IUserAgentService agent)
        {
            var platform = Platform(agent);
            var engine   = Engine(agent);
            return new BrowserService(agent, platform, engine);
        }

        #endregion

        #region Platform

        public static PlatformService Platform(string agent)
            => new PlatformService(UserAgent(agent));

        private static PlatformService Platform(IUserAgentService agent)
            => new PlatformService(agent);

        #endregion

        #region Engine

        public static EngineService Engine(string agent)
            => Engine(UserAgent(agent));

        private static EngineService Engine(IUserAgentService agent)
            => new EngineService(agent, Platform(agent));

        #endregion

        #region Crawler

        public static CrawlerService Crawler(string agent)
            => Crawler(agent, new DetectionOptions());

        public static CrawlerService Crawler(string agent, DetectionOptions options)
            => new CrawlerService(UserAgent(agent), options);

        #endregion

        #region Device

        public static DeviceService Device(string value, string header)
            => new DeviceService(UserAgent(value, header));

        public static DeviceService Device(string agent)
            => new DeviceService(UserAgent(agent));

        #endregion

        #region UserAgent

        private static IUserAgentService UserAgent(string agent)
            => MockUserAgentService(agent).Object;

        private static IUserAgentService UserAgent(string value, string header)
            => MockUserAgentService(value, header).Object;

        #endregion

        #region HttpContext

        public static IHttpContextService HttpContext(string agent)
            => MockHttpContextService(agent).Object;

        #endregion

        #region Internal

        private static IHttpContextAccessor HttpContextAccessor(string agent, string header = null)
        {
            var accessor = new HttpContextAccessor {HttpContext = CreateContext(agent, header)};
            return accessor;
        }

        private static HttpContext CreateContext(string agent)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("User-Agent", new[] {agent});
            return context;
        }

        private static HttpContext CreateContext(string value, string header)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Add(header, new[] {value});
            return context;
        }

        private static Mock<IUserAgentService> MockUserAgentService(string value, string header)
            => CreateContext(value, header)
                .SetupUserAgent(null);

        private static Mock<IUserAgentService> MockUserAgentService(string agent)
            => CreateContext(agent)
                .SetupUserAgent(agent);

        private static Mock<IHttpContextService> MockHttpContextService(string agent)
            => CreateContext(agent).SetupHttpContextService();

        private static Mock<IUserAgentService> SetupUserAgent(this UserAgent agent)
        {
            var service = new Mock<IUserAgentService>();
            service.Setup(f => f.UserAgent)
                   .Returns(agent);
            return service;
        }

        private static Mock<IHttpContextService> SetupHttpContextService(this HttpContext context)
        {
            var service = new Mock<IHttpContextService>();
            service.Setup(f => f.Context)
                   .Returns(context);
            return service;
        }

        #endregion
    }
}