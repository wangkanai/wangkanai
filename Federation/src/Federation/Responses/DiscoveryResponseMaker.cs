// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Responses;

public class DiscoveryResponseMaker : IDiscoveryResponseMaker
{
	private readonly FederationOptions _options;

	public DiscoveryResponseMaker(
		FederationOptions options)
	{
		_options = options;
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