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
}