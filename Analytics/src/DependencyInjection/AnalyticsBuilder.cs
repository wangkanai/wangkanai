// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Helper functions for configuring analytics services.
/// </summary>
public sealed class AnalyticsBuilder : IAnalyticsBuilder
{
	/// <summary>
	///     Creates a new instance of <see cref="AnalyticsBuilder" />
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
	public AnalyticsBuilder(IServiceCollection services) 
		=> Services = services.ThrowIfNull();

	/// <summary>
	///     Gets the <see cref="IServiceCollection" /> services are attached to.
	/// </summary>
	public IServiceCollection Services { get; }
}