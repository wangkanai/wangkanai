// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Webserver;

internal static class WebserverConstants
{
	public static class CacheControl
	{
		public const string ControlKey   = "Cache-Control";
		public const string ControlValue = "no-cache, no-store, max-age=0, must-revalidate";

		public const string PragmaKey   = "Pragma";
		public const string PragmaValue = "no-cache";

		public static string ControlMaxAge(int maxAge)
		{
			return $"max-age={maxAge}";
		}
	}

	public static class Vary
	{
		public const string Key = "Vary";
	}
}