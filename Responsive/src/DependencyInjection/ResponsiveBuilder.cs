// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Helper function for configuring responsive services</summary>
public class ResponsiveBuilder : IResponsiveBuilder
{
	/// <summary>Create a new instance of <see cref="IResponsiveBuilder" /></summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to attach to</param>
	public ResponsiveBuilder(IServiceCollection services)
		=> Services = services.ThrowIfNull();

	/// <summary>Gets the <see cref="IServiceCollection" /> services are attached to</summary>
	/// <value>The <see cref="IServiceCollection" /> services are attached to</value>
	public IServiceCollection Services { get; }
}
