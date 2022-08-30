// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Wangkanai.Responsive;
using Wangkanai.Responsive.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class ResponsiveApplicationExtensions
{
    public static IApplicationBuilder UseResponsive(this IApplicationBuilder app)
    {
        Check.NotNull(app);

        var options = app.ApplicationServices.GetRequiredService<ResponsiveOptions>();
        var context = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>()?.HttpContext;

        ValidateOptions(options);

        if (options.Disable)
            return app;

        // if (context.IsWebApi(options))
        //     return app;

        app.UseSession();
        app.UseMiddleware<ResponsiveMiddleware>();

        return app;
    }

    private static bool IsWebApi(this HttpContext context, ResponsiveOptions options)
    {
        return context.Request.Path.StartsWithSegments(options.WebApiPath);
    }

    private static void ValidateOptions(ResponsiveOptions options)
    {
        if (options.Disable && options.IncludeWebApi)
            throw new InvalidOperationException("IncludeWebApi is not needed if already Disable");
    }
}