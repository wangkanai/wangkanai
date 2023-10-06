// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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