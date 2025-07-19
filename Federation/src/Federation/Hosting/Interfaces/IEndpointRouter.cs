// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation.Hosting;

/// <summary>
/// Federation endpoint router contract
/// </summary>
public interface IEndpointRouter
{
	IEndpointHandler Find(HttpContext context);
}
