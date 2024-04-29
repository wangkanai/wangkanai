// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class BinaryScalingExtensions
{
	private static readonly string[] Sizes = ["B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];

	[DebuggerStepThrough]
	public static string ToHumanReadable(this short length)
		=> ((long)length).ToHumanReadable();

	[DebuggerStepThrough]
	public static string ToHumanReadable(this int length)
		=> ((long)length).ToHumanReadable();

	[DebuggerStepThrough]
	public static string ToHumanReadable(this long length)
	{
		var order = 0;
		// positive
		if (length >= 0)
			while (length >= 1024 && order + 1 < Sizes.Length)
			{
				order++;
				length /= 1024;
			}

		// negative
		if (length < 0)
			while (length <= -1024 && order + 1 < Sizes.Length)
			{
				order++;
				length /= 1024;
			}

		return $"{length:0.##} {Sizes[order]}";
	}

	[DebuggerStepThrough]
	public static string ToHumanReadable(this ulong length)
	{
		var order = 0;
		while (length >= 1024 && order + 1 < Sizes.Length)
		{
			order++;
			length /= 1024;
		}

		return $"{length:0.##} {Sizes[order]}";
	}
}
