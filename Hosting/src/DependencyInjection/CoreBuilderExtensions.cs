// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Wangkanai.Hosting.DependencyInjection;

public static class CoreBuilderExtensions
{
	public static void AddMarkerService<T>(this IServiceCollection services)
		where T : class
	{
		services.TryAddSingleton<T>();
	}
}