// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using icrosoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai;
using Wangkanai.Security.ApplicationModels;

#nullable disable

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthorizationCollectionExtensions
{
	public static IServiceCollection AddAuthorization(
		this IServiceCollection services,
		Action<AuthorizationOptions> setupAction,
		Action<SecurityOptions> configure)
	{
		services.ThrowIfNull();
		configure.ThrowIfNull();

		if (setupAction == null)
			services.AddAuthorization();
		else
			services.AddAuthorization(setupAction);

		services.Configure(configure);

		services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, PrivateNetworkApplicationModelProvider>());

		return services;
	}
}
