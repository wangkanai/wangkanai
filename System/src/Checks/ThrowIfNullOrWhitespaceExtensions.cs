// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfNullOrWhitespaceExtensions
{
	public static string ThrowIfNullOrWhitespace(this string? value)
	{
		return value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>();
	}

	public static string ThrowIfNullOrWhitespace(this string? value, string message)
	{
		return value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(message);
	}

	public static string ThrowIfNullOrWhitespace<T>(this string? value)
		where T : ArgumentException
	{
		return value.ThrowIfNullOrWhitespace<T>(nameof(value));
	}

	public static string ThrowIfNullOrWhitespace<T>(this string? value, string message)
		where T : ArgumentException
	{
		return value.ThrowIfNullOrWhitespace<T>(message, nameof(value));
	}

	public static string ThrowIfNullOrWhitespace<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
	{
		return value.IsNullOrWhiteSpace()
			       ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			       : value!;
	}
}
