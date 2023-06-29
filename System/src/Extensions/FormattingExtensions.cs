// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class FormattingExtensions
{
	private static readonly string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

	[DebuggerStepThrough]
	public static string ToHumanReadable(this int length)
		=> ((long)length).ToHumanReadable();

	[DebuggerStepThrough]
	public static string ToHumanReadable(this long length)
	{
		int order = 0;
		while (length >= 1024 && order + 1 < sizes.Length)
		{
			order++;
			length /= 1024;
		}

		return $"{length:0.##} {sizes[order]}";
	}

	[DebuggerStepThrough]
	public static string ToHumanReadable(this ulong length)
	{
		int order = 0;
		while (length >= 1024 && order + 1 < sizes.Length)
		{
			order++;
			length /= 1024;
		}

		return $"{length:0.##} {sizes[order]}";
	}
}