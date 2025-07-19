// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation.Hosting;

/// <summary>
/// Federation endpoint router contract
/// </summary>
public interface IEndpointRouter
{
	IEndpointHandler Find(HttpContext context);
}
