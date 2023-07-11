// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Wangkanai.Testing;
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
		var services    = new ServiceCollection();
		var builder     = services.AddWebmasterBuilder().AddRequiredServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptions<>), typeof(WebmasterOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsSnapshot<>), typeof(WebmasterOptions), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IOptionsMonitor<>), typeof(WebmasterOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsFactory<>), typeof(WebmasterOptions), ServiceLifetime.Transient));
		descriptors.Add(new(typeof(IOptionsMonitorCache<>), typeof(WebmasterOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(WebmasterOptions), typeof(WebmasterOptions), ServiceLifetime.Singleton));
		
		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddCoreServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddWebmasterBuilder().AddCoreServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IWebmasterService), typeof(WebmasterService), ServiceLifetime.Scoped));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddWebmaster_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddWebmaster());
	}

	[Fact]
	public void AddWebmasterBuilder_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddWebmasterBuilder());
	}
}