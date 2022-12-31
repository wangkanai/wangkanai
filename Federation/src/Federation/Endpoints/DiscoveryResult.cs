// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Federation.Hosting;

namespace Wangkanai.Federation.Endpoints;


/// <summary>
/// Discovery document return result
/// </summary>
public class DiscoveryResult : IEndpointResult
{
	public Dictionary<string, object> Records { get; }
	public int                        MaxAge  { get; }

	public DiscoveryResult(Dictionary<string, object> records, int maxAge)
	{
		Records = records.ThrowIfNull();
		MaxAge  = maxAge;
	}

	public Task ExecuteAsync(HttpContext context)
	{
		if (MaxAge >= 0)
			context.Response.SetCache(MaxAge, FederationConstants.Discovery.Origin);
		
		return context.Response.WriteAsJsonAsync(Records);
	}
}