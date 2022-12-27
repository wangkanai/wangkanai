// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Analytics.Hosting;

public sealed class AnalyticsMiddleware
{
    private readonly RequestDelegate _next;

    public AnalyticsMiddleware(RequestDelegate next)
    {
        _next = next.ThrowIfNull();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.ThrowIfNull();

        await _next(context);
    }
}