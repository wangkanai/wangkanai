// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Hosting.DependencyInjection;
using Wangkanai.Hosting.Services;

namespace Wangkanai.Hosting.Tests;

public class CoreBuilderTests
{
	[Fact]
	public void AddMarkerService()
	{
		var services = new ServiceCollection();
		services.AddMarkerService<MarkerService>();
		var provider = services.BuildServiceProvider();
		var marker = provider.GetService<MarkerService>();
		Assert.NotNull(marker);
	}

	[Fact]
	public void AddMarkerService_Validate()
	{
		var services = new ServiceCollection();
		services.AddMarkerService<MarkerService>();
		var provider = services.BuildServiceProvider();
	}
}
