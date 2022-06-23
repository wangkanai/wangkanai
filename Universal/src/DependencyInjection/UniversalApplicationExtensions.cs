// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;

using Wangkanai.Universal.Hosting;
using Wangkanai.Universal.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class UniversalApplicationExtensions
{
    public static IApplicationBuilder UseGoogleAnalytics(this IApplicationBuilder app)
    {
        Check.NotNull(app);

        app.Validate();
        app.VerifyMarkerIsRegistered<UniversalMarkerService>();

        return app.UseMiddleware<UniversalMiddleware>();
    }
    
    private static void Validate(this IApplicationBuilder app)
    {
        // What should I validate?
    }
}