// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Numerics;

namespace Wangkanai;

/// <summary>
/// Provides extension methods for throwing an <see cref="ArgumentOutOfRangeException"/> if a value is out of range.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfOutOfRangeExtension
{
	/// <summary>
	/// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified value is out of the specified range.
	/// </summary>
	/// <typeparam name="T">The type of the numeric value.</typeparam>
	/// <param name="index">The value to check.</param>
	/// <param name="lower">The lower bound of the range.</param>
	/// <param name="upper">The upper bound of the range.</param>
	/// <returns>The specified value if it is within the specified range.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when the specified value is out of the specified range.</exception>
	public static T ThrowIfOutOfRange<T>([NotNull] this T index, T lower, T upper)
		where T : IBinaryInteger<T>
		=> index < lower || index >= upper
			   ? throw new ArgumentOutOfRangeException(nameof(index))
			   : index;
}
