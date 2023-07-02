// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Hosting;
using Wangkanai.Webmaster.Services;

using Xunit;

namespace Wangkanai.Webmaster;

public class CoreBuilderExtensionsTests
{
	[Fact]
	public void AddMarkerServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddWebmasterBuilder().AddMarkerService();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(WebmasterMarkerService), typeof(WebmasterMarkerService), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddRequiredPlatformServices_ReturnsExpected()
	{
		var services = new ServiceCollection();
		var builder  = services.AddWebmasterBuilder();
		Assert.Same(services, builder.Services);
	}
}