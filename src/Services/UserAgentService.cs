// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

public class UserAgentService : IUserAgentService
{
    public UserAgent UserAgent { get; }

    public UserAgentService(IHttpContextService context)
        => UserAgent = new UserAgent(context.Request.Headers["User-Agent"].FirstOrDefault() ?? "");
}