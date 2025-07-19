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
		var device = DeviceService(agent);
		return ResponsiveService(accessor, device, options);
	}

	internal static ResponsiveService ResponsiveService(IHttpContextAccessor accessor, IDeviceService device, ResponsiveOptions options = null!)
		=> new(accessor, device, options);


	internal static DeviceService DeviceService(string agent)
		=> new(UserAgentService(agent));


	internal static IUserAgentService UserAgentService(string agent)
		=> UserAgentService(new UserAgent(agent));

	internal static IUserAgentService UserAgentService(UserAgent agent)
		=> MockUserAgentService(agent).Object;


	internal static IHttpContextService HttpContextService(string agent)
		=> MockHttpContextService(agent).Object;


	private static Mock<IUserAgentService> MockUserAgentService(string agent)
		=> MockUserAgentService(new UserAgent(agent));

	private static Mock<IUserAgentService> MockUserAgentService(UserAgent agent)
	{
		var service = new Mock<IUserAgentService>();
		service.Setup(a => a.UserAgent).Returns(agent);
		return service;
	}

	internal static Mock<IHttpContextService> MockHttpContextService(string agent)
		=> CreateHttpContext(agent).SetupHttpContextService();

	private static Mock<IHttpContextService> SetupHttpContextService(this HttpContext context)
	{
		var service = new Mock<IHttpContextService>();
		service.Setup(f => f.Context)
			   .Returns(context);
		return service;
	}

	internal static IHttpContextAccessor MockHttpContextAccessor()
		=> new MockHttpContextAccessor { HttpContext = CreateHttpContext() };

	internal static IHttpContextAccessor MockHttpContextAccessor(string agent)
		=> new MockHttpContextAccessor { HttpContext = CreateHttpContext(agent) };

	private static IHttpContextAccessor CreateHttpContextAccessor(string agent)
		=> new HttpContextAccessor { HttpContext = CreateHttpContext(agent) };

	private static HttpContext CreateHttpContext(string value)
	{
		var context = new DefaultHttpContext();
		context.Request.Headers.Append("User-Agent", new[] { value });
		return context;
	}

	private static HttpContext CreateHttpContext()
		=> new DefaultHttpContext();
}
