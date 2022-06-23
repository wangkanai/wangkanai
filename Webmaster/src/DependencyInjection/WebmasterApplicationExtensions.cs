// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;

using Wangkanai.Webmaster;
using Wangkanai.Webmaster.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension method for adding the <see cref="WebmasterMiddleware"/> to the application.
/// </summary>
public static class WebmasterApplicationExtensions
{
    public static IApplicationBuilder UseWebmaster(this IApplicationBuilder app)
    {
        Check.NotNull(app);

        app.Validate();
        app.VerifyMarkerIsRegistered<WebmasterMarkerService>();

        return app.UseMiddleware<WebmasterMiddleware>();
    }

    private static void Validate(this IApplicationBuilder app)
    {
        // What should I validate?
    }
}