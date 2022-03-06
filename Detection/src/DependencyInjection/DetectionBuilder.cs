// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Helper function for configuring detection services
/// </summary>
public class DetectionBuilder : IDetectionBuilder
{
    /// <summary>
    /// Creates a new instance of <see cref="IDetectionBuilder" />
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
    public DetectionBuilder(IServiceCollection services)
        => Services = Check.NotNull(services);

    /// <summary>
    /// Gets the <see cref="IServiceCollection" /> services are attached to
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection" /> services are attached to
    /// </value>
    public IServiceCollection Services { get; }
}