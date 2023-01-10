// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Graph.Handlers;
using Wangkanai.Graph.Providers;

namespace Wangkanai.Graph.DependencyInjection;

public static class GraphClientExtensions
{
	public static IServiceCollection AddGraphClient(this IServiceCollection services, params string[] scopes)
	{
		services.Configure<RemoteAuthenticationOptions<MsalProviderOptions>>(options => {
			foreach (var scope in scopes) options.ProviderOptions.AdditionalScopesToConsent.Add(scope);
		});

		services.AddScoped<IAuthenticationProvider, NoOpGraphAuthenticationProvider>();
		services.AddScoped<IHttpProvider, HttpClientHttpProvider>(sp => new HttpClientHttpProvider(new HttpClient()));
		services.AddScoped(sp => {
			var authenticationProvider = sp.GetRequiredService<IAuthenticationProvider>();
			var httpProvider           = sp.GetRequiredService<IHttpProvider>();
			return new GraphServiceClient(authenticationProvider, httpProvider);
		});
		services.AddScoped<GraphApiAuthorizationMessageHandler>();

		return services;
	}
}