// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

/// <summary>
/// Provides extension methods for scaling binary values to human-readable formats.
/// </summary>
[DebuggerStepThrough]
public static class BinaryScalingExtensions
{
	private static readonly string[] Sizes = ["B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];

	/// <summary>
	/// Converts a binary value to human-readable format.
	/// </summary>
	/// <param name="length">The binary value to convert.</param>
	/// <returns>The human-readable format of the binary value.</returns>
	public static string ToHumanReadable(this short length)
		=> ((long)length).ToHumanReadable();

	/// <summary>
	/// Converts a binary value to human-readable format.
	/// </summary>
	/// <param name="length">The binary value to convert.</param>
	/// <returns>The human-readable format of the binary value.</returns>
	public static string ToHumanReadable(this int length)
		=> ((long)length).ToHumanReadable();

	/// <summary>
	/// Converts a binary value to human-readable format.
	/// </summary>
	/// <param name="length">The binary value to convert.</param>
	/// <returns>The human-readable format of the binary value.</returns>
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

	/// <summary>
	/// Converts a binary value to human-readable format.
	/// </summary>
	/// <param name="length">The binary value to convert.</param>
	/// <returns>The human-readable format of the binary value.</returns>
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
