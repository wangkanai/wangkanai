// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Webmaster;

public sealed class WebmasterMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = next.ThrowIfNull();

	public async Task InvokeAsync(HttpContext context)
	{
		context.ThrowIfNull();

		await _next(context).ConfigureAwait(false);
	}
}
