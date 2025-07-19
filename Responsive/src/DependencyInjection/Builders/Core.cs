// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Responsive;
using Wangkanai.Responsive.Hosting;
using Wangkanai.Responsive.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ResponsiveCoreBuilderExtensions
{
	public static IResponsiveBuilder AddRequiredServices(this IResponsiveBuilder builder)
	{
		builder.ThrowIfNull();

		builder.Services.AddHttpContextAccessor();
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<ResponsiveOptions>>().Value);

		builder.Services.AddDetection();

		return builder;
	}

	public static IResponsiveBuilder AddCoreServices(this IResponsiveBuilder builder)
	{
		builder.ThrowIfNull();

		return builder;
	}

	public static IResponsiveBuilder AddResponsiveService(this IResponsiveBuilder builder)
	{
		builder.ThrowIfNull();

		builder.AddSessionServices();
		builder.Services.TryAddTransient<IResponsiveService, ResponsiveService>();
		builder.Services.AddRazorViewLocation();
		builder.Services.AddRazorPagesConventions();

		return builder;
	}

	public static IResponsiveBuilder AddSessionServices(this IResponsiveBuilder builder)
	{
		builder.ThrowIfNull();

		builder.Services.AddDistributedMemoryCache();
		builder.Services.AddSession(
			options =>
			{
				options.Cookie.Name = "Responsive";
				options.IdleTimeout = TimeSpan.FromSeconds(10);
				options.Cookie.IsEssential = true;
			});

		return builder;
	}

	internal static IResponsiveBuilder AddMarkerService(this IResponsiveBuilder builder)
	{
		builder.Services.TryAddSingleton<ResponsiveMarkerService>();

		return builder;
	}

	private static IServiceCollection AddRazorViewLocation(this IServiceCollection services)
	{
		return services.Configure<RazorViewEngineOptions>(options =>
		{
			options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix));
			//options.ViewLocationExpanders.Add(new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Subfolder));
			options.ViewLocationExpanders.Add(new ResponsivePageLocationExpander());
		});
	}

	private static IServiceCollection AddRazorPagesConventions(this IServiceCollection services)
	{
		services.AddRazorPages(options => { options.Conventions.Add(new ResponsivePageRouteModelConvention()); });

		services.AddSingleton<MatcherPolicy, ResponsivePageMatcherPolicy>();

		return services;
	}


}
