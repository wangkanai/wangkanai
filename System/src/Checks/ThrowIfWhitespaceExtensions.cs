// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfWhitespaceExtensions
{
	public static string ThrowIfWhitespace(this string? value)
	{
		return value.ThrowIfWhitespace<ArgumentWhitespaceException>();
	}

	public static string ThrowIfWhitespace(this string? value, string message)
	{
		return value.ThrowIfWhitespace<ArgumentWhitespaceException>(message);
	}

	public static string ThrowIfWhitespace<T>(this string? value)
		where T : ArgumentException
	{
		return value.ThrowIfWhitespace<T>(nameof(value));
	}

	public static string ThrowIfWhitespace<T>(this string? value, string message)
		where T : ArgumentException
	{
		return value.ThrowIfWhitespace<T>(message, nameof(value));
	}

	public static string ThrowIfWhitespace<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
	{
		return value.IsWhiteSpace()
			       ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			       : value!;
	}
}
