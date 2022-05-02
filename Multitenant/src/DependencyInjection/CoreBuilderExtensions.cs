// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Multitenant.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class MultitenantCoreBuilderExtensions
{
    public static IMultitenantBuilder AddMarkerService(this IMultitenantBuilder builder)
    {
        builder.Services.TryAddSingleton<MultitenantMarkerService>();

        return builder;
    }
}