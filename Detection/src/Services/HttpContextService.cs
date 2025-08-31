// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services;

public sealed class HttpContextService : IHttpContextService
{
   public HttpContextService(IHttpContextAccessor accessor)
   {
      accessor.ThrowIfNull();

      Context = accessor?.HttpContext ?? new DefaultHttpContext();
   }

   public HttpContext Context { get; }
   public HttpRequest Request => Context.Request;
}