// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Wangkanai.Federation.Hosting;

/// <summary>
/// Federation Middleware
/// </summary>
public sealed class FederationMiddleware
{
	private readonly ILogger<FederationMiddleware> _logger;
	private readonly RequestDelegate _next;

	/// <summary>
	/// Initializes a new instance of the <see cref="FederationMiddleware"/> class.
	/// </summary>
	/// <param name="next">The next request delegate.</param>
	/// <param name="logger">The logger</param>
	public FederationMiddleware(RequestDelegate next, ILogger<FederationMiddleware> logger)
	{
		_next = next.ThrowIfNull();
		_logger = logger.ThrowIfNull();
	}

	public async Task InvokeAsync(
		HttpContext context,
		FederationOptions options,
		IEndpointRouter router)
	{
		context.ThrowIfNull();

		// Perform federation logic here

		await _next(context);
	}
}
