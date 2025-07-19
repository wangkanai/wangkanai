// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Wangkanai.Hosting.DependencyInjection;

public static class CoreBuilderExtensions
{
	public static void AddMarkerService<T>(this IServiceCollection services)
		where T : class
		=> services.TryAddSingleton<T>();
}
