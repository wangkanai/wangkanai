// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai;

[DebuggerStepThrough]
public static class NullEqualityComparerExtensions
{
	/// <summary>Determines whether the specified value is equal to null or the default value of the type.</summary>
	/// <typeparam name="T">The type of the value to compare.</typeparam>
	/// <param name="value">The value to be compared.</param>
	/// <returns>True if the specified value is null or the default for the type; otherwise, false.</returns>
	public static bool EqualNull<T>(this T? value)
		=> EqualityComparer<T>.Default.Equals(value, default);

	/// <summary>Determines whether the specified value is not equal to null or the default value of the type.</summary>
	/// <typeparam name="T">The type of the value to compare.</typeparam>
	/// <param name="value">The value to be compared.</param>
	/// <returns>True if the specified value is not null and not the default for the type; otherwise, false.</returns>
	public static bool NotNull<T>(this T? value)
		=> !value.EqualNull();
}
