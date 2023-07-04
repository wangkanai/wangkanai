// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

/// <summary>
/// Configures which endpoints are enabled or disabled.
/// </summary>
public sealed class EndpointsOptions
{
	public bool EnableDiscoveryEndpoint { get; set; } = true;
}