// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Pipeline extension methods for adding Analytics
/// </summary>
public static class AnalyticsApplicationExtensions
{
    public static IApplicationBuilder UseAnalytics(this IApplicationBuilder app)
    {
        Check.NotNull(app);
        
        // todo
        
        return app;
    }
}