// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Webserver.Hosting;

public sealed class WebserverMiddleware
{
    private readonly RequestDelegate _next;

    public WebserverMiddleware(RequestDelegate next)
        => _next = next.IfNullThrow();

    public async Task InvokeAsync(HttpContext context)
    {
        context.IfNullThrow();

        await _next(context).ConfigureAwait(false);
    }
}