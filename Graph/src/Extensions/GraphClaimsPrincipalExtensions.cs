// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Graph.Extensions;

public static class GraphClaimsPrincipalExtensions
{
    public static string GetUserGraphEmail(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.FindFirst(GraphClaimTypes.Email);
        return claim?.Value;
    }

    public static string GetUserGraphPhoto(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.FindFirst(GraphClaimTypes.Photo);
        return claim?.Value;
    }

    public static string GetUserGraphJobTitle(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.FindFirst(GraphClaimTypes.JobTitle);
        return claim?.Value;
    }
}