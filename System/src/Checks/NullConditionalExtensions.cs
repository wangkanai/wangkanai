// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

#pragma warning disable CS8777 // Parameter must have a non-null value when exiting.

namespace Wangkanai;

/// <summary>
/// Provides extension methods for null conditional operations.
/// </summary>
[DebuggerStepThrough]
public static class NullConditionalExtensions
{
	/// <summary>
	/// Checks if the value is null and returns true if it is null; otherwise, returns false.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>True if the value is null; otherwise, false.</returns>
	public static bool TrueIfNull<T>([NotNull] this T value)
		=> value is null;

	/// <summary>
	/// Checks if the value is not null and returns false if it is not null; otherwise, returns true.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>False if the value is not null; otherwise, true.</returns>
	public static bool FalseIfNull<T>([NotNull] this T value)
		=> value is not null;
}
