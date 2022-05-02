// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

public static class MultitenantCollectionExtensions
{
    public static IMultitenantBuilder AddMultitenant(this IServiceCollection services)
        => services.AddMultitenantBuilder()
                   .AddMarkerService();

    internal static IMultitenantBuilder AddMultitenantBuilder(this IServiceCollection services)
        => new MultitenantBuilder(services);
}