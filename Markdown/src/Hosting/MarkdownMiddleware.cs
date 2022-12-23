// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Markdown.Hosting;

public sealed class MarkdownMiddleware
{
    private readonly RequestDelegate _next;

    public MarkdownMiddleware(RequestDelegate next)
    {
        _next = next.IfNullThrow();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.IfNullThrow();

        await _next(context);
    }
}