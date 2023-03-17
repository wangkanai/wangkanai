// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Responses;

/// <summary>
/// Discovery endpoint response maker contract
/// </summary>
public interface IDiscoveryResponseFactory
{
	Task<Dictionary<string, object>> CreateResultAsync(string baseUri, string issuerUri);

	Task<IEnumerable<JsonWebKey>> CreateJwkAsync();
}