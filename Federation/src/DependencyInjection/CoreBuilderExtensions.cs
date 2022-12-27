// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Federation.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class FederationCoreBuilderExtensions
{
    internal static IFederationBuilder AddMarkerService(this IFederationBuilder builder)
    {
        builder.Services.TryAddSingleton<FederationMarkerService>();

        return builder;
    }
}