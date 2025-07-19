// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation;

/// <summary>
/// Options class to configuration the discovery endpoint
/// </summary>
public sealed class DiscoveryOptions
{
	/// <summary>
	/// Sets the max age in seconds of the cache control header of the HTTP response.
	/// This gives clients hint how often they should refresh their cached copy of the discovery document.
	/// If set to 0 no-cache headers will be set. Defaults to 3600, which is 1 hour.
	/// </summary>
	public int ResponseCacheMaxAge { get; set; } = 3600;
}
