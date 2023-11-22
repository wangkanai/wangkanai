// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai;
using Wangkanai.Testing;
using Wangkanai.Testing.Builders;

namespace Microsoft.Extensions.DependencyInjection;

internal static class AssertionBuilderExtensions
{
	internal static IAssertionBuilder AddRequiredService(this IAssertionBuilder builder)
	{
		builder.ThrowIfNull();
		builder.Services.AddHttpContextAccessor();
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<AssertionOptions>>().Value);
		return builder;
	}

	internal static IAssertionBuilder AddLifetimeService(this IAssertionBuilder builder)
	{
		builder.Services.TryAddSingleton<ISingletonService, SingletonService>();
		builder.Services.TryAddScoped<IScopedService, ScopedService>();
		builder.Services.TryAddTransient<ITransientService, TransientService>();
		return builder;
	}

	internal static IAssertionBuilder AddMockService(this IAssertionBuilder builder)
	{
		builder.Services.TryAddSingleton<IAssertionService, AssertionService>();
		return builder;
	}

	// For internal unit tests
	internal static IAssertionBuilder AddMarkerService(this IAssertionBuilder builder)
	{
		builder.Services.TryAddSingleton<AssertionMarkerService>();
		return builder;
	}
}
