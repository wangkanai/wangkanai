// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Wangkanai.Webmaster;

public class WebmasterCollectionExtensionTests
{
	[Fact]
	public void AddRequiredPlatformServices_ReturnsExpected()
	{
		var services = new ServiceCollection();
		var builder  = services.AddWebmasterBuilder();
		Assert.Same(services, builder.Services);
	}
	
	[Fact]
	public void AddRequiredPlatformServices_OnlyAddsOnce()
	{
		var services = new ServiceCollection();
		services.AddWebmasterBuilder();
		services.AddWebmasterBuilder();
		Assert.Single(services);
	}
}