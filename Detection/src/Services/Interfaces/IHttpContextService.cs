// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services;

/// <summary>
///     Provides the APIs for HttpContext.
/// </summary>
public interface IHttpContextService
{
    /// <summary>
    ///     Get the <see cref="HttpContext" /> of the application service.
    /// </summary>
    HttpContext Context { get; }

    HttpRequest Request { get; }
}