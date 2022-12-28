// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;

using Wangkanai.Federation.AspNetIdentity;

namespace Microsoft.Extensions.DependencyInjection;

public static class FederationBuilderExtensions
{
    public static IFederationBuilder AddAspNetIdentity<TUser>(this IFederationBuilder builder)
        where TUser : class
    {
        builder.Services.AddTransientDecorator<IUserClaimsPrincipalFactory<TUser>, UserClaimsFactory<TUser>>();

        return builder;
    }
}