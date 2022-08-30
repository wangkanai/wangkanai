// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Wangkanai.Graph.Handlers;

public class GraphApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public GraphApiAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation)
        : base(provider, navigation)
    {
        ConfigureHandler(
            new[] { "https://graph.microsoft.com" },
            new[] { "https://graph.microsoft.com/User.Read" });
    }
}