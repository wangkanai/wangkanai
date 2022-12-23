// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services;

public sealed class HttpContextService : IHttpContextService
{
    public HttpContextService(IHttpContextAccessor accessor)
    {
        accessor.IfNullThrow();

        Context = accessor?.HttpContext ?? new DefaultHttpContext();
    }

    private HttpContextService()
    {
        Context = new DefaultHttpContext();
    }

    public HttpContext Context { get; }
    public HttpRequest Request => Context.Request;
}