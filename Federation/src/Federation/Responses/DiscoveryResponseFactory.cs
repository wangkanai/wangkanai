// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Logging;

using Wangkanai.Federation.Models;
using Wangkanai.Federation.Services;
using Wangkanai.Identity;
using Wangkanai.Internal;

namespace Wangkanai.Federation.Responses;

public class DiscoveryResponseFactory : IDiscoveryResponseFactory
{
	protected readonly FederationOptions                 Options;
	protected readonly IKeyMaterialService               Keys;
	protected readonly ILogger<DiscoveryResponseFactory> Logger;

	public DiscoveryResponseFactory(
		FederationOptions                 options,
		IKeyMaterialService               keys,
		ILogger<DiscoveryResponseFactory> logger)
	{
		Options = options;
		Keys    = keys;
		Logger  = logger;
	}

	public virtual async Task<Dictionary<string, object>> CreateResultAsync(string baseUri, string issuerUri)
	{
		using var activity = Tracing.BasicActivitySource.StartActivity();

		baseUri = baseUri.EnsureTrailingSlash();

		var entries = new Dictionary<string, object>
		{
			{ OidcConstants.Discovery.Issuer, issuerUri }
		};

		// to do work list

		return entries;
	}

	public virtual async Task<IEnumerable<JsonWebKey>> CreateJwkAsync()
	{
		using var activity = Tracing.BasicActivitySource.StartActivity();

		var webKeys = new List<JsonWebKey>();

		foreach (var key in await Keys.GetValidationKeysAsync())
		{
			// to do work list
		}

		return webKeys;
	}
}