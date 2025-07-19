// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Markdown.Hosting;

public sealed class MarkdownMiddleware
{
	private readonly RequestDelegate _next;

	public MarkdownMiddleware(RequestDelegate next)
	{
		_next = next.ThrowIfNull();
	}

	public async Task InvokeAsync(HttpContext context)
	{
		context.ThrowIfNull();

		await _next(context);
	}
}
