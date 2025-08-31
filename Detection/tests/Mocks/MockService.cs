// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

using Moq;

using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Mocks;

[DebuggerStepThrough]
public static class MockService
{
   #region Device

   internal static DeviceService DeviceService(string agent)
      => new(UserAgentService(agent));

   #endregion

   #region HttpContextService

   internal static IHttpContextService HttpContextService(string agent)
      => MockHttpContextService(agent).Object;

   #endregion

   #region Browser

   internal static BrowserService BrowserService(string agent)
      => BrowserService(UserAgentService(agent));

   private static BrowserService BrowserService(IUserAgentService agent)
   {
      var platform = PlatformService(agent);
      var engine   = EngineService(agent);
      return new(agent, engine);
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
      => CrawlerService(agent, new());

   internal static CrawlerService CrawlerService(string agent, DetectionOptions options)
      => new(UserAgentService(agent), options);

   #endregion

   #region UserAgent

   internal static IUserAgentService UserAgentService(string agent)
      => UserAgentService(new UserAgent(agent));

   internal static IUserAgentService UserAgentService(UserAgent agent)
      => MockUserAgentService(agent).Object;

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

   internal static IHttpContextAccessor MockHttpContextAccessor(string agent)
      => new MockHttpContextAccessor { HttpContext = CreateHttpContext(agent) };

   internal static IHttpContextAccessor CreateHttpContextAccessor(string agent)
      => new HttpContextAccessor { HttpContext = CreateHttpContext(agent) };

   private static HttpContext CreateHttpContext(string value)
   {
      var context = new DefaultHttpContext();
      context.Request.Headers.Append("User-Agent", new[] { value });
      return context;
   }

   #endregion
}