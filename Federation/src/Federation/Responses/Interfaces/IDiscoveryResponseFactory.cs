// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
