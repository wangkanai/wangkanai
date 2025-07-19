// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Federation.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class FederationRunnableBuilderExtensions
{
	public static IFederationBuilder AddRunnableServices(this IFederationBuilder builder)
	{
		builder.Services.TryAddTransient<IKeyMaterialService, FederationKeyMaterialService>();

		return builder;
	}
}
