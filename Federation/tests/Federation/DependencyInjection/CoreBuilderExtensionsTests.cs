// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using Wangkanai.Federation;
using Wangkanai.Federation.Services;
using Wangkanai.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public class CoreBuilderExtensionsTests
{
	[Fact]
	public void AddFederation_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddFederation());
	}
	
	[Fact]
	public void AddFederationBuilder_Null_ArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddFederationBuilder());
	}

	[Fact]
	public void AddMarkerServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddFederationBuilder().AddMarkerService();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(FederationMarkerService), typeof(FederationMarkerService), ServiceLifetime.Singleton));
		
		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
	
	[Fact]
	public void AddRequiredPlatformServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddFederationBuilder().AddRequiredServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptions<>), typeof(FederationOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsSnapshot<>), typeof(FederationOptions), ServiceLifetime.Scoped));
		descriptors.Add(new(typeof(IOptionsMonitor<>), typeof(FederationOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(IOptionsFactory<>), typeof(FederationOptions), ServiceLifetime.Transient));
		descriptors.Add(new(typeof(IOptionsMonitorCache<>), typeof(FederationOptions), ServiceLifetime.Singleton));
		descriptors.Add(new(typeof(FederationOptions), typeof(FederationOptions), ServiceLifetime.Singleton));
		
		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
	
	[Fact]
	public void AddCoreServices_ReturnsExpected()
	{
		var services    = new ServiceCollection();
		var builder     = services.AddFederationBuilder().AddCoreServices();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IServerUris), typeof(FederationServerUris), ServiceLifetime.Transient));
		descriptors.Add(new(typeof(IIssuerNameService), typeof(FederationIssuerNameService), ServiceLifetime.Transient));

		
		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
}