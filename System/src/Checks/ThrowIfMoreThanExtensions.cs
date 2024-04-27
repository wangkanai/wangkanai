// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

public static class ThrowIfMoreThanExtensions
{
	public static bool ThrowIfMoreThan([NotNull] this int value, int expected)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected);

	public static bool ThrowIfMoreThan([NotNull] this int value, int expected, string message)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, message);

	public static bool ThrowIfMoreThan<T>([NotNull] this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfMoreThan<T>(expected, nameof(value));

	public static bool ThrowIfMoreThan<T>([NotNull] this int value, int expected, string message)
		where T : ArgumentException
		=> value > expected
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(message)
			   : true;
}
