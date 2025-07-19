// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Markdown.Builder;
using Wangkanai.Markdown.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extension method to <see cref="IServiceCollection" /> for configuring markdown services.</summary>
public static class MarkdownCollectionExtensions
{
	/// <summary> Add Markdown service to the services container</summary>
	/// <param name="services">The services available in the application</param>
	/// <returns>An <see cref="IMarkdownBuilder" /> so that additional calls can be chained</returns>
	public static IMarkdownBuilder AddMarkdownPages(this IServiceCollection services)
		=> services.AddMarkdownBuilder()
				   .AddRequiredServices()
				   .AddCoreServices()
				   .AddMarkerService();

	/// <summary>Add Markdown service to the services container</summary>
	/// <param name="services">The services available in the application</param>
	/// <param name="setAction">An <see cref="Action{MarkdownOptions}" /> to configure the provided <see cref="AnalyticsOptions" /></param>
	/// <returns>An <see cref="IMarkdownBuilder" /> so that additional calls can be chained</returns>
	public static IMarkdownBuilder AddMarkdownPages(this IServiceCollection services, Action<MarkdownPagesOptions> setAction)
		=> services.Configure(setAction)
				   .AddMarkdownPages();

	// For internal unit tests
	internal static IMarkdownBuilder AddMarkdownBuilder(this IServiceCollection services)
		=> new MarkdownBuilder(services);
}
