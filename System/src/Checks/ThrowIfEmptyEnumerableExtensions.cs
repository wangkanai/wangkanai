// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using System.Diagnostics.CodeAnalysis;

using Wangkanai.Exceptions;

#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.

namespace Wangkanai;

[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
[DebuggerStepThrough]
public static class ThrowIfEmptyExtensions
{
	public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T>? value)
	{
		return !value.ThrowIfNull().Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<ArgumentEmptyException>(nameof(value))
			       : value;
	}

	public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T>? value, string message)
	{
		return !value.ThrowIfNull().Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<ArgumentEmptyException>(nameof(value), message)
			       : value;
	}

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>(this IEnumerable<TType>? value)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<TException>(nameof(value))
			       : value;
	}

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>(this IEnumerable<TType>? value, string message)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw ExceptionActivator.CreateArgumentInstance<TException>(nameof(value), message)
			       : value;
	}
}
