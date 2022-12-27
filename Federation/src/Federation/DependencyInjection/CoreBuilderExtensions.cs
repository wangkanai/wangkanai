// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Federation;
using Wangkanai.Federation.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class FederationCoreBuilderExtensions
{
    internal static IFederationBuilder AddRequiredServices(this IFederationBuilder builder)
    {
        builder.ThrowIfNull();
        
        // Hosting doesn't add IHttpContextAccessor by default
        builder.Services.AddHttpContextAccessor();
        
        // Add Federation Options
        builder.Services.AddOptions();
        builder.Services.TryAddSingleton(p=>p.GetRequiredService<IOptions<FederationOptions>>().Value);
        
        return builder;
    }
    
    internal static IFederationBuilder AddCoreServices(this IFederationBuilder builder)
    {
        // Add basic core services
        
        return builder;
    }
    
    internal static IFederationBuilder AddMarkerService(this IFederationBuilder builder)
    {
        builder.Services.TryAddSingleton<FederationMarkerService>();

        return builder;
    }
}