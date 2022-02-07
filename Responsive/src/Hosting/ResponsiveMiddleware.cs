// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

using Wangkanai.Responsive.Extensions;
using Wangkanai.Responsive.Services;

namespace Wangkanai.Responsive.Hosting;

public class ResponsiveMiddleware
{
    private readonly RequestDelegate _next;

    public ResponsiveMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context, IResponsiveService responsive)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));

        context.SetDevice(responsive.View);

        await _next(context);
    }
}