// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Markdown.Hosting;

public class MarkdownMiddleware
{
    private readonly RequestDelegate _next;

    public MarkdownMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));

        await _next(context);
    }
}