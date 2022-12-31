// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation.Hosting;

/// <summary>
/// Federation endpoint handler contract
/// </summary>
public interface IEndpointHandler
{
	Task<IEndpointResult> ProcessAsync(HttpContext context);
}

/// <summary>
/// Federation endpoint result contract
/// </summary>
public interface IEndpointResult
{
	Task ExecuteAsync(HttpContext context);
}

/// <summary>
/// Federation endpoint router contract
/// </summary>
public interface IEndpointRouter
{
	IEndpointHandler Find(HttpContext context);
}