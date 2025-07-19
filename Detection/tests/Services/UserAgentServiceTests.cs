// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services;

public class UserAgentServiceTests
{
	[Fact]
	public void Ctor_IServiceProvider_Success()
	{
		var agent = "Agent";
		var context = new DefaultHttpContext();
		context.Request.Headers["User-Agent"] = agent;

		var accessor = new HttpContextAccessor { HttpContext = context };
		var contextService = new HttpContextService(accessor);

		var useragentService = new UserAgentService(contextService);

		Assert.NotNull(useragentService.UserAgent);
		Assert.Equal(agent, useragentService.UserAgent.ToString());
	}

	[Fact]
	public void Ctor_Null_ThrowsArgumentNullException()
	{
		//Assert.Throws<ArgumentNullException>(() => new UserAgentService(null));
	}

	[Fact]
	public void Ctor_HttpContextAccessorNotResolved_ThrowsArgumentNullException()
	{
		//Assert.Throws<ArgumentNullException>(() => new UserAgentService(new HttpContextAccessor()));
	}

	[Fact]
	public void Ctor_HttpContextNull_ThrowsArgumentNullException()
	{
		var accessor = new HttpContextAccessor();

		Assert.Null(accessor.HttpContext);
		//Assert.Throws<ArgumentNullException>(() => new UserAgentService(accessor));
	}
}
