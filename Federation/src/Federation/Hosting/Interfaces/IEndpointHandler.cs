// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation.Hosting;

/// <summary>
/// Federation endpoint handler contract
/// </summary>
public interface IEndpointHandler
{
	Task<IEndpointResult> ProcessAsync(HttpContext context);
}
