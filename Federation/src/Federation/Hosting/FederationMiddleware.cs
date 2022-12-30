// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation.Hosting;

public sealed class FederationMiddleware
{
    private readonly RequestDelegate _next;

    public FederationMiddleware(RequestDelegate next)
    {
        _next = next.ThrowIfNull();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.ThrowIfNull();

        // Perform federation logic here

        await _next(context);
    }
}