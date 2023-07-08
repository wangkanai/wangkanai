// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Wangkanai.Hosting;
using Wangkanai.Responsive.Services;

using Xunit;

namespace Wangkanai.Responsive.DependencyInjection;

public class CoreBuilderExtensionsTests
{
	[Fact]
	public void AddResponsive_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddResponsive());
	}
	
	[Fact]
	public void AddResponsiveBuilder_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddResponsiveBuilder());
	}
	
	[Fact]
	public void AddMarkerServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddResponsiveBuilder().AddMarkerService();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(ResponsiveMarkerService), typeof(ResponsiveMarkerService), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
	
	[Fact]
	public void AddRequiredPlatformServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddResponsiveBuilder().AddRequiredServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptions<>), typeof(ResponsiveOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsSnapshot<>), typeof(ResponsiveOptions), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IOptionsMonitor<>), typeof(ResponsiveOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsFactory<>), typeof(ResponsiveOptions), ServiceLifetime.Transient));
		descriptors.Add(new(typeof(IOptionsMonitorCache<>), typeof(ResponsiveOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(ResponsiveOptions), typeof(ResponsiveOptions), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddCoreServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddResponsiveBuilder().AddCoreServices();
		var descriptors = new List<ServiceDescriptor>();
		
		
		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
}