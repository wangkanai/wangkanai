// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Logging;

using Wangkanai.Federation.Models;
using Wangkanai.Identity;
using Wangkanai.Internal;

namespace Wangkanai.Federation.Responses;

public class DiscoveryResponseMaker : IDiscoveryResponseMaker
{
	private readonly FederationOptions               _options;
	private readonly ILogger<DiscoveryResponseMaker> _logger;

	public DiscoveryResponseMaker(
		FederationOptions               options,
		ILogger<DiscoveryResponseMaker> logger)
	{
		_options = options;
		_logger  = logger;
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

	public Task<IEnumerable<JsonWebKey>> CreateJwkAsync()
	{
		throw new NotImplementedException();
	}
}