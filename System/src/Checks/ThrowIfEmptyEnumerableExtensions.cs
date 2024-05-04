// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>
/// Provides extension methods to throw an exception if an enumerable collection is empty.
/// </summary>
[DebuggerStepThrough]
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public static class ThrowIfEmptyExtensions
{
	/// <summary>
	/// Throws an ArgumentEmptyException if the enumerable collection is empty.
	/// </summary>
	/// <typeparam name="T">The type of items in the collection</typeparam>
	/// <param name="value">The enumerable collection</param>
	/// <returns>The input enumerable collection if it is not empty</returns>
	/// <exception cref="ArgumentEmptyException">Thrown when the input enumerable collection is empty</exception>
	public static IEnumerable<T> ThrowIfEmpty<T>([NotNull] this IEnumerable<T> value)
		=> value.Any() ? value : throw new ArgumentEmptyException(nameof(value));

	/// <summary>
	/// Throws an ArgumentEmptyException if the enumerable collection is empty.
	/// </summary>
	/// <typeparam name="T">The type of items in the collection</typeparam>
	/// <param name="value">The enumerable collection</param>
	/// <returns>The input enumerable collection if it is not empty</returns>
	/// <exception cref="ArgumentEmptyException">Thrown when the input enumerable collection is empty</exception>
	public static IEnumerable<T> ThrowIfEmpty<T>([NotNull] this IEnumerable<T> value, string message)
		=> value.Any() ? value : throw new ArgumentEmptyException(nameof(value), message);

	/// <summary>
	/// Throws an ArgumentEmptyException if the enumerable collection is empty.
	/// </summary>
	/// <typeparam name="T">The type of items in the collection</typeparam>
	/// <param name="value">The enumerable collection</param>
	/// <returns>The input enumerable collection if it is not empty</returns>
	/// <exception cref="ArgumentEmptyException">Thrown when the input enumerable collection is empty</exception>
	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>([NotNull] this IEnumerable<TType>? value)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<TException>(nameof(value))
			       : value;
	}

	/// <summary>
	/// Throws an ArgumentEmptyException if the enumerable collection is empty.
	/// </summary>
	/// <typeparam name="T">The type of items in the collection</typeparam>
	/// <param name="value">The enumerable collection</param>
	/// <returns>The input enumerable collection if it is not empty</returns>
	/// <exception cref="ArgumentEmptyException">Thrown when the input enumerable collection is empty</exception>
	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>([NotNull] this IEnumerable<TType>? value, string message)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<TException>(nameof(value), message)
			       : value;
	}
}
