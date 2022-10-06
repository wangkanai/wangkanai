// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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
    {
        return new DeviceService(UserAgentService(agent));
    }

    #endregion

    #region HttpContextService

    internal static IHttpContextService HttpContextService(string agent)
    {
        return MockHttpContextService(agent).Object;
    }

    #endregion

    #region Browser

    internal static BrowserService BrowserService(string agent)
    {
        return BrowserService(UserAgentService(agent));
    }

    internal static BrowserService BrowserService(IUserAgentService agent)
    {
        var platform = PlatformService(agent);
        var engine   = EngineService(agent);
        return new BrowserService(agent, engine);
    }

    #endregion

    #region Platform

    internal static PlatformService PlatformService(string agent)
    {
        return new PlatformService(UserAgentService(agent));
    }

    internal static PlatformService PlatformService(IUserAgentService service)
    {
        return new PlatformService(service);
    }

    #endregion

    #region Engine

    internal static EngineService EngineService(string agent)
    {
        return EngineService(UserAgentService(agent));
    }

    internal static EngineService EngineService(IUserAgentService agent)
    {
        return new EngineService(agent, PlatformService(agent));
    }

    #endregion

    #region Crawler

    internal static CrawlerService CrawlerService(string agent)
    {
        return CrawlerService(agent, new DetectionOptions());
    }

    internal static CrawlerService CrawlerService(string agent, DetectionOptions options)
    {
        return new CrawlerService(UserAgentService(agent), options);
    }

    #endregion

    #region UserAgent

    internal static IUserAgentService UserAgentService(string agent)
    {
        return UserAgentService(new UserAgent(agent));
    }

    internal static IUserAgentService UserAgentService(UserAgent agent)
    {
        return MockUserAgentService(agent).Object;
    }

    #endregion

    #region Mocking

    private static Mock<IUserAgentService> MockUserAgentService(string agent)
    {
        return MockUserAgentService(new UserAgent(agent));
    }

    private static Mock<IUserAgentService> MockUserAgentService(UserAgent agent)
    {
        var service = new Mock<IUserAgentService>();
        service.Setup(a => a.UserAgent).Returns(agent);
        return service;
    }

    private static Mock<IHttpContextService> MockHttpContextService(string agent)
    {
        return CreateHttpContext(agent).SetupHttpContextService();
    }

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
    {
        return new MockHttpContextAccessor { HttpContext = CreateHttpContext(agent) };
    }

    internal static IHttpContextAccessor CreateHttpContextAccessor(string agent)
    {
        return new HttpContextAccessor { HttpContext = CreateHttpContext(agent) };
    }

    private static HttpContext CreateHttpContext(string value)
    {
        var context = new DefaultHttpContext();
        context.Request.Headers.Add("User-Agent", new[] { value });
        return context;
    }

    #endregion
}