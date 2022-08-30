// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

using Microsoft.AspNetCore.Http;

using Moq;

using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;
using Wangkanai.Responsive.Services;

namespace Wangkanai.Responsive.Mocks;

[DebuggerStepThrough]
public static class MockService
{
    internal static ResponsiveService ResponsiveService(string agent, ResponsiveOptions options = null!)
    {
        var accessor = CreateHttpContextAccessor(agent);
        var device   = DeviceService(agent);
        return ResponsiveService(accessor, device, options);
    }

    internal static ResponsiveService ResponsiveService(IHttpContextAccessor accessor, IDeviceService device, ResponsiveOptions options = null!)
    {
        return new(accessor, device, options);
    }


    internal static DeviceService DeviceService(string agent)
    {
        return new(UserAgentService(agent));
    }


    internal static IUserAgentService UserAgentService(string agent)
    {
        return UserAgentService(new UserAgent(agent));
    }

    internal static IUserAgentService UserAgentService(UserAgent agent)
    {
        return MockUserAgentService(agent).Object;
    }


    internal static IHttpContextService HttpContextService(string agent)
    {
        return MockHttpContextService(agent).Object;
    }


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

    internal static Mock<IHttpContextService> MockHttpContextService(string agent)
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

    internal static IHttpContextAccessor MockHttpContextAccessor()
    {
        return new MockHttpContextAccessor { HttpContext = CreateHttpContext() };
    }

    internal static IHttpContextAccessor MockHttpContextAccessor(string agent)
    {
        return new MockHttpContextAccessor { HttpContext = CreateHttpContext(agent) };
    }

    private static IHttpContextAccessor CreateHttpContextAccessor(string agent)
    {
        return new HttpContextAccessor { HttpContext = CreateHttpContext(agent) };
    }

    private static HttpContext CreateHttpContext(string value)
    {
        var context = new DefaultHttpContext();
        context.Request.Headers.Add("User-Agent", new[] { value });
        return context;
    }

    private static HttpContext CreateHttpContext()
    {
        return new DefaultHttpContext();
    }
}