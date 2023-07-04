// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

internal static class Constants
{
	public static class EndpointNames
	{
		public const string Discovery = "Discovery";
	}

	public static class ProtocolRoutePaths
	{
		public const string ConnectPathPrefix = "connect";

		public const string DiscoveryConfiguration = ".well-known/openid-configuration";
	}

	public static class EnvironmentKeys
	{
		public const string SignOutCalled = "federation:SignOutCalled";
	}
}