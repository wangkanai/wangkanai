// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Hosting;
using Wangkanai.Federation.Options;

namespace Wangkanai.Federation;

/// <summary>
/// Provides programmatic configuration for the <see cref="FederationMiddleware" />.
/// </summary>
public sealed class FederationOptions
{
	public DiscoveryOptions       Discovery        { get; set; } = new();
	public IssuerOptions          Issuer           { get; set; } = new();
	public EndpointsOptions       Endpoints        { get; set; } = new();
	public ConfigurationOptions   Stores           { get; set; } = new();
	public ValidationOptions      Validation       { get; set; } = new();
	public CorsOptions            Cors             { get; set; } = new();
	public CspOptions             Csp              { get; set; } = new();
	public CachingOptions         Caching          { get; set; } = new();
	public AuthenticationOptions  Authentication   { get; set; } = new();
	public PersistentGrantOptions PersistentGrants { get; set; } = new();
}