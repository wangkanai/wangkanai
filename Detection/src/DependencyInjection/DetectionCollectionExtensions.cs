// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Contains extension method to <see cref="IServiceCollection" /> for configuring client services
/// </summary>
public static class DetectionCollectionExtensions
{
	/// <summary>Add Detection service to the services container</summary>
	/// <param name="services">The services available in the application</param>
	/// <returns>An <see cref="IDetectionBuilder" /> so that additional calls can be chained</returns>
	public static IDetectionBuilder AddDetection(this IServiceCollection services)
		=> services.AddDetectionBuilder()
		           .AddRequiredServices()
		           .AddCoreServices()
		           .AddMarkerService();

	/// <summary>Add Detection service to the services container</summary>
	/// <param name="services">The services available in the application</param>
	/// <param name="setAction">An <see cref="Action{DetectionOptions}" /> to configure the provided <see cref="DetectionOptions" /></param>
	/// <returns>An <see cref="IDetectionBuilder" /> so that additional calls can be chained</returns>
	public static IDetectionBuilder AddDetection(this IServiceCollection services, Action<DetectionOptions> setAction)
		=> services.Configure(setAction)
		           .AddDetection();

	internal static IDetectionBuilder AddDetectionBuilder(this IServiceCollection services)
		=> new DetectionBuilder(services);
}