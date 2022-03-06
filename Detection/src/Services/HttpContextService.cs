// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services;

public class HttpContextService : IHttpContextService
{
    public HttpContext Context { get; }
    public HttpRequest Request => Context.Request;

    public HttpContextService(IHttpContextAccessor accessor)
    {
        if (accessor == null) throw new ArgumentNullException(nameof(accessor));
        if (accessor == null) throw new ArgumentNullException(nameof(accessor));
        if (accessor == null) throw new ArgumentNullException(nameof(accessor));
        if (accessor == null) throw new ArgumentNullException(nameof(accessor));
        Context = accessor?.HttpContext ?? new DefaultHttpContext();
    }

    private HttpContextService()
        => Context = new DefaultHttpContext();
}