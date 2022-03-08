// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Analytics.Hosting;

public class AnalyticsMiddleware
{
    private readonly RequestDelegate _next;

    public AnalyticsMiddleware(RequestDelegate next) 
        => _next = Check.NotNull(next);
    
    public async Task InvokeAsync(HttpContext context)
    {
        Check.NotNull(context);

        await _next(context);
    }
}