// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation;

public static class FederationConstants
{
	public const string DefaultCookieAuthenticationScheme = "federation";
	public const string LocalIdentityProvider = "federation";
	public const string ExternalAuthenticationMethod = "federation.external";
	public const string DefaultCheckSessionCookieName = "federation.session";

	public static readonly TimeSpan DefaultCookieLifetime = TimeSpan.FromHours(10);
	public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(48);

	public static class Discovery
	{
		public const string Origin = "Origin";
	}

	public static class ProtocolTypes
	{
		public const string OpenIdConnect = "oidc";
	}

	public static class LocalApi
	{
		public const string AuthenticationScheme = "FederationAccessToken";
		public const string PolicyName = AuthenticationScheme;
	}
}
