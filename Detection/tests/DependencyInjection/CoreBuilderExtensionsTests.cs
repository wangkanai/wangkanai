// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Wangkanai.Detection.Services;
using Wangkanai.Hosting;

namespace Wangkanai.Detection.DependencyInjection;

public class CoreBuilderExtensionsTests
{
	[Fact]
	public void AddDetection_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddDetection());
	}

	[Fact]
	public void AddDetectionBuilder_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddDetectionBuilder());
	}
	
	[Fact]
	public void AddMarkerServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddDetectionBuilder().AddMarkerService();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(DetectionMarkerService), typeof(DetectionMarkerService), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddRequiredPlatformServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddDetectionBuilder().AddRequiredServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptions<>), typeof(DetectionOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsSnapshot<>), typeof(DetectionOptions), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IOptionsMonitor<>), typeof(DetectionOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsFactory<>), typeof(DetectionOptions), ServiceLifetime.Transient));
		descriptors.Add(new(typeof(IOptionsMonitorCache<>), typeof(DetectionOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(DetectionOptions), typeof(DetectionOptions), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddCoreServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddDetectionBuilder().AddCoreServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IHttpContextService), typeof(HttpContextService), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IUserAgentService), typeof(UserAgentService), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IDeviceService), typeof(DeviceService), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IEngineService), typeof(EngineService), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IPlatformService), typeof(PlatformService), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IBrowserService), typeof(BrowserService), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(ICrawlerService), typeof(CrawlerService), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IDetectionService), typeof(DetectionService), ServiceLifetime.Scoped));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
}