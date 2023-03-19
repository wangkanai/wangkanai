// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

public static class FederationConstants
{
	public const string DefaultCookieAuthenticationScheme = "federation";
	public const string LocalIdentityProvider             = "federation";
	public const string ExternalAuthenticationMethod      = "external";

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
		public const string PolicyName           = AuthenticationScheme;
	}
}