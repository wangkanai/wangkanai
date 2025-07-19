// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Webserver;

internal static class WebserverConstants
{
	public static class CacheControl
	{
		public const string ControlKey = "Cache-Control";
		public const string ControlValue = "no-cache, no-store, max-age=0, must-revalidate";

		public const string PragmaKey = "Pragma";
		public const string PragmaValue = "no-cache";

		public static string ControlMaxAge(int maxAge) => $"max-age={maxAge}";
	}

	public static class Vary
	{
		public const string Key = "Vary";
	}
}
