// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfNegativeExtensions
{
	public static int ThrowIfNegative([NotNull] this int value)
		=> value.ThrowIfNegative<ArgumentNegativeException>();

	public static int ThrowIfNegative([NotNull] this int value, string message)
		=> value.ThrowIfNegative<ArgumentNegativeException>(message);

	public static int ThrowIfNegative<T>([NotNull] this int value)
		where T : ArgumentException
		=> value.ThrowIfNegative<T>(nameof(value));

	public static int ThrowIfNegative<T>([NotNull] this int value, string message)
		where T : ArgumentException
		=> value < 0
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			   : value;
}
