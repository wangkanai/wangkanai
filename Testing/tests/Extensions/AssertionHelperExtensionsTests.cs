// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing.Tests.Extensions;

public class AssertionHelperExtensionsTests
{
	[Fact]
	public void Null_Services()
	{
		ServiceCollection       services    = null!;
		List<ServiceDescriptor> descriptors = new List<ServiceDescriptor>();
		descriptors.AssertServices(services);
		Assert.Null(services);
		Assert.Empty(descriptors);
	}

	[Fact]
	public void Null_Descriptors()
	{
		ServiceCollection       services    = new ServiceCollection();
		List<ServiceDescriptor> descriptors = null!;
		Assert.Throws<NullReferenceException>(() => descriptors.AssertServices(services));
	}

	[Fact]
	public void Empty_Services_Descriptors()
	{
		var services    = new ServiceCollection();
		var descriptors = new List<ServiceDescriptor>();

		Assert.Empty(services);
		Assert.Empty(descriptors);
		descriptors.AssertServices(services);
		
	}

	[Fact]
	public void Assert_Singleton()
	{
		var services = new ServiceCollection();
		services.AddSingleton<IAssertionService, AssertionService>();

		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(IAssertionService), typeof(AssertionService), ServiceLifetime.Singleton));

		Assert.NotNull(services);
		descriptors.AssertServices(services);
	}

	[Fact]
	public void Assert_Scoped()
	{
		var services = new ServiceCollection();
		services.AddScoped<IAssertionService, AssertionService>();

		var descriptors = new List<ServiceDescriptor>();
		descriptors.AssertServices(services);

		Assert.Single(services);
		//Assert.Single(descriptors);
	}

	[Fact]
	public void Assert_Transient()
	{
		var services = new ServiceCollection();
		services.AddTransient<IAssertionService, AssertionService>();

		var descriptors = new List<ServiceDescriptor>();
		descriptors.AssertServices(services);

		Assert.Single(services);
		//Assert.Single(descriptors);
	}

	[Fact]
	public void Assert_Multiple()
	{
		var services = new ServiceCollection();
		services.AddSingleton<ISingletonService, SingletonService>();
		services.AddScoped<IScopedService, ScopedService>();
		services.AddTransient<ITransientService, TransientService>();

		var descriptors = new List<ServiceDescriptor>();
		descriptors.AssertServices(services);
	}

	[Fact]
	public void Assert_Builder()
	{
		var services = new ServiceCollection();
		var builder  = services.AddAssertionBuilder().AddMarkerService();
		var descriptors = new List<ServiceDescriptor>();
		descriptors.Add(new(typeof(AssertionMarkerService), typeof(AssertionMarkerService), ServiceLifetime.Singleton));

		Assert.NotNull(builder);
		Assert.NotNull(builder.Services);
		descriptors.AssertServices(builder.Services);
	}
}