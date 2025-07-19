// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Wangkanai.Detection.Services;
using Wangkanai.Testing;

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
		var services = new ServiceCollection();
		var builder = services.AddDetectionBuilder().AddMarkerService();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new ServiceDescriptor(typeof(DetectionMarkerService), typeof(DetectionMarkerService), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddRequiredPlatformServices_ReturnsExpected()
	{
		var services = new ServiceCollection();
		var builder = services.AddDetectionBuilder().AddRequiredServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new ServiceDescriptor(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton));
		descriptors.Add(new ServiceDescriptor(typeof(IOptions<>), typeof(DetectionOptions), ServiceLifetime.Singleton));
		descriptors.Add(new ServiceDescriptor(typeof(IOptionsSnapshot<>), typeof(DetectionOptions), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(IOptionsMonitor<>), typeof(DetectionOptions), ServiceLifetime.Singleton));
		descriptors.Add(new ServiceDescriptor(typeof(IOptionsFactory<>), typeof(DetectionOptions), ServiceLifetime.Transient));
		descriptors.Add(new ServiceDescriptor(typeof(IOptionsMonitorCache<>), typeof(DetectionOptions), ServiceLifetime.Singleton));
		descriptors.Add(new ServiceDescriptor(typeof(DetectionOptions), typeof(DetectionOptions), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}

	[Fact]
	public void AddCoreServices_ReturnsExpected()
	{
		var services = new ServiceCollection();
		var builder = services.AddDetectionBuilder().AddCoreServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new ServiceDescriptor(typeof(IHttpContextService), typeof(HttpContextService), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(IUserAgentService), typeof(UserAgentService), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(IDeviceService), typeof(DeviceService), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(IEngineService), typeof(EngineService), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(IPlatformService), typeof(PlatformService), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(IBrowserService), typeof(BrowserService), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(ICrawlerService), typeof(CrawlerService), ServiceLifetime.Scoped));
		descriptors.Add(new ServiceDescriptor(typeof(IDetectionService), typeof(DetectionService), ServiceLifetime.Scoped));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
}
