// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai;
using Wangkanai.Detection;
using Wangkanai.Detection.Services;
using Wangkanai.Responsive.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class ResponsiveCoreBuilderExtensions
{
    public static IResponsiveBuilder AddRequiredPlatformServices(this IResponsiveBuilder builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        // Hosting doesn't add IHttpContextAccessor by default
        builder.Services.AddHttpContextAccessor();

        // Add Detection Options
        builder.Services.AddOptions();
        builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<ResponsiveOptions>>().Value);

        return builder;
    }

    public static IResponsiveBuilder AddCoreServices(this IResponsiveBuilder builder)
    {
        // Add core to services
        builder.Services.AddDetection();

        return builder;
    }

    public static IResponsiveBuilder AddResponsiveService(this IResponsiveBuilder builder)
    {
        Check.NotNull(builder);

        builder.AddSessionServices();
        builder.Services.TryAddTransient<IResponsiveService, ResponsiveService>();
        builder.Services.AddRazorViewLocation();
        builder.Services.AddRazorPagesConventions();

        return builder;
    }

    public static IResponsiveBuilder AddSessionServices(this IResponsiveBuilder builder)
    {
        // Add Session to services
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(
            options =>
            {
                options.Cookie.Name        = "Responsive";
                options.IdleTimeout        = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

        return builder;
    }

    private static IServiceCollection AddRazorViewLocation(this IServiceCollection services)
        => services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix));
            //options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Subfolder));
            options.ViewLocationExpanders.Add(new ResponsivePageLocationExpander());
        });

    private static IServiceCollection AddRazorPagesConventions(this IServiceCollection services)
    {
        services.AddRazorPages(options => { options.Conventions.Add(new ResponsivePageRouteModelConvention()); });

        services.AddSingleton<MatcherPolicy, ResponsivePageMatcherPolicy>();

        return services;
    }
}