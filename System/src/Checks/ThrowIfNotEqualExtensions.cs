// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfNotEqualExtensions
{
	public static bool ThrowIfNotEqual([NotNull] this int value, int expected)
		=> value.ThrowIfNotEqual<ArgumentNotEqualException>(expected, nameof(value));

	public static bool ThrowIfNotEqual<T>([NotNull] this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfNotEqual<T>(expected, nameof(value));

	public static bool ThrowIfNotEqual<T>([NotNull] this int value, int expected, string paramName)
		where T : ArgumentException
		=> value != expected
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName)
			   : true;
}
