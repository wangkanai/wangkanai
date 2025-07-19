// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation;

/// <summary>
/// Configures which endpoints are enabled or disabled.
/// </summary>
public sealed class EndpointsOptions
{
	public bool EnableDiscoveryEndpoint { get; set; } = true;
}
