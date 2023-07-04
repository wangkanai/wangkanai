// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Federation;
using Wangkanai.Federation.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class FederationCoreBuilderExtensions
{
	internal static IFederationBuilder AddRequiredServices(this IFederationBuilder builder)
	{
		builder.ThrowIfNull();

		// Hosting doesn't add IHttpContextAccessor by default
		builder.Services.AddHttpContextAccessor();

		// Add Federation Options
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(p => p.GetRequiredService<IOptions<FederationOptions>>().Value);
		builder.Services.AddHttpClient();

		return builder;
	}

	internal static IFederationBuilder AddCoreServices(this IFederationBuilder builder)
	{
		// Add basic core services
		builder.Services.AddTransient<IServerUris, FederationServerUris>();
		builder.Services.AddTransient<IIssuerNameService, FederationIssuerNameService>();

		return builder;
	}

	internal static IFederationBuilder AddMarkerService(this IFederationBuilder builder)
	{
		builder.Services.TryAddSingleton<FederationMarkerService>();

		return builder;
	}
}