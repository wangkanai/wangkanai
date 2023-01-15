// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Logging;

using Wangkanai.Federation.Models;

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

	public Task<Dictionary<string, object>> CreateResultAsync(string issuerUri, string baseUri)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<JsonWebKey>> CreateJwkAsync()
	{
		throw new NotImplementedException();
	}
}