// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Webserver.Hosting;

public class WebserverMiddleware
{
    private readonly RequestDelegate _next;

    public WebserverMiddleware(RequestDelegate next)
        => _next = Check.NotNull(next);

    public async Task InvokeAsync(HttpContext context)
    {
        Check.NotNull(context);

        await _next(context).ConfigureAwait(false);
    }
}