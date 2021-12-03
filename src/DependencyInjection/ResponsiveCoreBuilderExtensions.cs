// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Detection;
using Wangkanai.Detection.Hosting;
using Wangkanai.Detection.Services;

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
        builder.Services.TryAddSingleton(
            provider => provider.GetRequiredService<IOptions<ResponsiveOptions>>().Value);

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
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        builder.Services.TryAddTransient<IResponsiveService, ResponsiveService>();
        builder.AddSessionServices();
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
                options.Cookie.Name        = "Detection";
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