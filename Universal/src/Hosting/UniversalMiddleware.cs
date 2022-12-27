// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Universal.Hosting;

public sealed class UniversalMiddleware
{
    private readonly RequestDelegate _next;

    public UniversalMiddleware(RequestDelegate next)
    {
        _next = next.ThrowIfNull();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.ThrowIfNull();

        await _next(context).ConfigureAwait(false);
    }
}