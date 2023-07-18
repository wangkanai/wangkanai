// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

internal static class DomainConstants
{
	public static class ProtocolTypes
	{
		public const string OpenIdConnect = "oidc";
		public const string OAuth2        = "oauth2";
	}

	public static class SecretTypes
	{
		public const string SharedSecret = "SharedSecret";
	}

	public static class StandardScopes
	{
		public const string OpenId        = "openid";
		public const string Profile       = "profile";
		public const string Email         = "email";
		public const string Address       = "address";
		public const string Phone         = "phone";
		public const string OfflineAccess = "offline_access";
	}
}