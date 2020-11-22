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

        internal static ResponsiveService ResponsiveService(string agent, DetectionOptions options = null!)
        {
            var accessor = CreateHttpContextAccessor(agent);
            var device   = DeviceService(agent);
            return ResponsiveService(accessor, device, options);
        }

        internal static ResponsiveService ResponsiveService(IHttpContextAccessor accessor, IDeviceService device, DetectionOptions options = null!)
            => new(accessor, device, options);

        #endregion

        #region Browser

        internal static BrowserService BrowserService(string agent)
            => BrowserService(UserAgentService(agent));

        internal static BrowserService BrowserService(IUserAgentService agent)
        {
            var platform = PlatformService(agent);
            var engine   = EngineService(agent);
            return new BrowserService(agent, platform, engine);
        }

        #endregion

        #region Platform

        internal static PlatformService PlatformService(string agent)
            => new(UserAgentService(agent));

        internal static PlatformService PlatformService(IUserAgentService service)
            => new(service);

        #endregion

        #region Engine

        internal static EngineService EngineService(string agent)
            => EngineService(UserAgentService(agent));

        internal static EngineService EngineService(IUserAgentService agent)
            => new(agent, PlatformService(agent));

        #endregion

        #region Crawler

        internal static CrawlerService CrawlerService(string agent)
            => CrawlerService(agent, new DetectionOptions());

        internal static CrawlerService CrawlerService(string agent, DetectionOptions options)
            => new(UserAgentService(agent), options);

        #endregion

        #region Device

        internal static DeviceService DeviceService(string agent)
            => new(UserAgentService(agent));

        #endregion

        #region UserAgent

        internal static IUserAgentService UserAgentService(string agent)
            => UserAgentService(new UserAgent(agent));

        internal static IUserAgentService UserAgentService(UserAgent agent)
            => MockUserAgentService(agent).Object;

        #endregion

        #region HttpContextService

        internal static IHttpContextService HttpContextService(string agent)
            => MockHttpContextService(agent).Object;

        #endregion

        #region Mocking

        private static Mock<IUserAgentService> MockUserAgentService(string agent)
            => MockUserAgentService(new UserAgent(agent));
        
        private static Mock<IUserAgentService> MockUserAgentService(UserAgent agent)
        {
            var service = new Mock<IUserAgentService>();
            service.Setup(a => a.UserAgent).Returns(agent);
            return service;
        }

        private static Mock<IHttpContextService> MockHttpContextService(string agent)
            => CreateHttpContext(agent).SetupHttpContextService();

        private static Mock<IHttpContextService> SetupHttpContextService(this HttpContext context)
        {
            var service = new Mock<IHttpContextService>();
            service.Setup(f => f.Context)
                   .Returns(context);
            return service;
        }

        #endregion

        #region Internal

        private static IHttpContextAccessor CreateHttpContextAccessor(string agent) 
            => new HttpContextAccessor
            {
                HttpContext = CreateHttpContext(agent)
            };

        private static HttpContext CreateHttpContext(string value)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("User-Agent", new[] {value});
            return context;
        }

        #endregion
    }
}