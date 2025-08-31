// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Analytics.Hosting;

public sealed class AnalyticsMiddleware
{
   private readonly RequestDelegate _next;

   public AnalyticsMiddleware(RequestDelegate next) => _next = next.ThrowIfNull();

   public async Task InvokeAsync(HttpContext context)
   {
      context.ThrowIfNull();

      await _next(context);
   }
}