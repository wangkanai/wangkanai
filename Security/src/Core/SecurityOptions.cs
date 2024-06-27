// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Security;

namespace icrosoft.AspNetCore.Authorization;

/// <summary>
/// Provides programmatic configuration for the ASP.NET Core security framework.
/// </summary>
public class SecurityOptions
{
    private Dictionary<string, PrivateNetworkPolicy> PolicyMap                  { get; }      = new();
    public  bool                                     InvokeHandlersAfterFailure { get; set; } = true;
    public  PrivateNetworkPolicy                     DefaultNetwork             { get; set; }
    public  PrivateNetworkPolicy?                    FallbackNetwork            { get; set; }

    public void AddNetwork(string name, PrivateNetworkPolicy policy)
    {
        name.ThrowIfNull();
        policy.ThrowIfNull();

        PolicyMap[name] = policy;
    }

    public PrivateNetworkPolicy? GetNetwork(string name)
    {
        name.ThrowIfNull();

        return PolicyMap.TryGetValue(name, out var policy)
                   ? policy
                   : null;
    }
}