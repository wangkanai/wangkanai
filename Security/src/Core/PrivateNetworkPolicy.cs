// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Security;

public class PrivateNetworkPolicy
{
    public IReadOnlyList<IAuthorizationRequirement> Requirements          { get; }
    public IReadOnlyList<string>                    AuthenticationSchemes { get; }

    public PrivateNetworkPolicy(IEnumerable<IAuthorizationRequirement> requirements, IEnumerable<string> authenticationSchemes)
    {
        requirements.ThrowIfNull();
        authenticationSchemes.ThrowIfNull();

        if (!requirements.Any())
            throw new InvalidOperationException(SecurityResources.Exception_PrivateNetworkPolicyEmpty);

        Requirements          = new List<IAuthorizationRequirement>(requirements).AsReadOnly();
        AuthenticationSchemes = new List<string>(authenticationSchemes).AsReadOnly();
    }
}