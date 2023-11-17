// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfEqualExtensions
{
	public static bool ThrowIfEqual(this int value, int expected)
	{
		return value.ThrowIfEqual<ArgumentEqualException>(expected, nameof(value));
	}

	public static bool ThrowIfEqual<T>(this int value, int expected)
		where T : ArgumentException
	{
		return value.ThrowIfEqual<T>(expected, nameof(value));
	}

	public static bool ThrowIfEqual<T>(this int value, int expected, [InvokerParameterName] string paramName)
		where T : ArgumentException
	{
		return value == expected
			       ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName)
			       : false;
	}
}
