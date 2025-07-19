// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Wangkanai.Extensions;

namespace Wangkanai.Hosting;

[DebuggerStepThrough]
public static class StringExtensions
{
	public static string CleanUrlPath([NotNull] this string url)
	{
		if (url.IsNullOrWhiteSpace())
			url = "/";
		if (url != "/" && url.EndsWith('/'))
			url = url.Substring(0, url.Length - 1);

		return url;
	}
}
