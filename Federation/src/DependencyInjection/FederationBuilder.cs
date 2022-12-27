// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Helper function for configuring federation services.
/// </summary>
public sealed class FederationBuilder : IFederationBuilder
{
    /// <summary>
    /// Get the <see cref="IServiceCollection"/> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection"/> services are attached to.
    /// </value>
    public IServiceCollection Services { get; }

    
    /// <summary>
    /// Create a new instance of <see cref="IFederationBuilder"/>.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to attached to.
    /// </param>
    public FederationBuilder(IServiceCollection services)
        => Services = services;
}