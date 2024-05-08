// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

/// <summary>
/// Static class containing extension methods for throwing ArgumentNullException if a value is null.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfNullGenericExtensions
{
	/// <summary>
	/// Throws an ArgumentNullException if the value is null.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
	public static T ThrowIfNull<T>([NotNull] this T value)
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value));

	/// <summary>
	/// Throws an ArgumentNullException if the value is null.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value to check for null.</param>
	/// <param name="message">The custom error message.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
	public static T ThrowIfNull<T>([NotNull] this T value, string message)
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value), message);
}
