// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Helper function for configuring detection services.</summary>
public sealed class DetectionBuilder : IDetectionBuilder
{
	/// <summary>Creates a new instance of <see cref="IDetectionBuilder" /></summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
	public DetectionBuilder(IServiceCollection services)
		=> Services = services.ThrowIfNull();

	/// <summary>Gets the <see cref="IServiceCollection" /> services are attached to</summary>
	/// <value>The <see cref="IServiceCollection" /> services are attached to</value>
	public IServiceCollection Services { get; }
}
