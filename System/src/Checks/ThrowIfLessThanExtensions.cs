// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfLessThanExtensions
{
	public static bool ThrowIfLessThan(this int value, int expected)
	{
		return value.ThrowIfLessThan<ArgumentLessThanException>(expected, nameof(value));
	}

	public static bool ThrowIfLessThan(this int value, int expected, string message)
	{
		return value.ThrowIfLessThan<ArgumentLessThanException>(expected, message);
	}

	public static bool ThrowIfLessThan<T>(this int value, int expected)
		where T : ArgumentException
	{
		return value.ThrowIfLessThan<T>(expected, nameof(value));
	}

	public static bool ThrowIfLessThan<T>(this int value, int expected, string message)
		where T : ArgumentException
	{
		return value < expected
			       ? throw ExceptionActivator.CreateArgumentInstance<T>(message)
			       : true;
	}
}
