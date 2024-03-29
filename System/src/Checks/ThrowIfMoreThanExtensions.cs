// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

public static class ThrowIfMoreThanExtensions
{
	public static bool ThrowIfMoreThan(this int value, int expected)
	{
		return value.ThrowIfMoreThan<ArgumentMoreThanException>(expected);
	}

	public static bool ThrowIfMoreThan(this int value, int expected, string message)
	{
		return value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, message);
	}

	public static bool ThrowIfMoreThan<T>(this int value, int expected)
		where T : ArgumentException
	{
		return value.ThrowIfMoreThan<T>(expected, nameof(value));
	}

	public static bool ThrowIfMoreThan<T>(this int value, int expected, string message)
		where T : ArgumentException
	{
		return value > expected
			       ? throw ExceptionActivator.CreateArgumentInstance<T>(message)
			       : true;
	}
}
