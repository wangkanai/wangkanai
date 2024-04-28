// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
[DebuggerStepThrough]
public static class ThrowIfEmptyExtensions
{
	public static IEnumerable<T> ThrowIfEmpty<T>([NotNull] this IEnumerable<T> value)
		=> value.Any() ? value : throw new ArgumentEmptyException(nameof(value));

	public static IEnumerable<T> ThrowIfEmpty<T>([NotNull] this IEnumerable<T> value, string message)
		=> value.Any() ? value : throw new ArgumentEmptyException(nameof(value), message);

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>([NotNull] this IEnumerable<TType>? value)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<TException>(nameof(value))
			       : value;
	}

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>([NotNull] this IEnumerable<TType>? value, string message)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<TException>(nameof(value), message)
			       : value;
	}
}
