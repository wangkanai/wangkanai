// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Net.Http.Headers;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Wangkanai.Graph.Providers;

internal class NoOpGraphAuthenticationProvider : IAuthenticationProvider
{
    public IAccessTokenProvider TokenProvider { get; }

    public NoOpGraphAuthenticationProvider(IAccessTokenProvider tokenProvider)
    {
        TokenProvider = tokenProvider;
    }

    public async Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
        var result = await TokenProvider.RequestAccessToken(new AccessTokenRequestOptions()
        {
            Scopes = new[] { "https://graph.microsoft.com/User.Read" }
        });

        if (result.TryGetToken(out var token))
            request.Headers.Authorization ??= new AuthenticationHeaderValue("Bearer", token.Value);
    }
}