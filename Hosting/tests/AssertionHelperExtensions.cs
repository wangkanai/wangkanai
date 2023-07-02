// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Hosting;

public static class AssertionHelperExtensions
{
	public static void AssertServices(this List<ServiceDescriptor> descriptors, IServiceCollection services)
	{
		for (var i = 0; i < descriptors.Count; i++)
		{
			Assert.Equal(descriptors[i].ServiceType, services[i].ServiceType);
			Assert.Equal(descriptors[i].ImplementationType, services[i].ImplementationType);
			Assert.Equal(descriptors[i].Lifetime, services[i].Lifetime);
		}
	}
}