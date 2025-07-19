// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Federation.Responses;

namespace Microsoft.Extensions.DependencyInjection;

public static class FederationResponseBuilderExtensions
{
	public static IFederationBuilder AddResponseMakers(this IFederationBuilder builder)
	{
		builder.Services.TryAddTransient<IDiscoveryResponseFactory, DiscoveryResponseFactory>();

		return builder;
	}
}
