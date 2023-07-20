// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

using Wangkanai.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

[DebuggerStepThrough]
public static class StringExtensions
{
	public static string CleanUrlPath(this string url)
	{
		if (url.IsNullOrWhiteSpace())
			url = "/";
		if (url != "/" && url.EndsWith("/"))
			url = url.Substring(0, url.Length - 1);

		return url;
	}
}