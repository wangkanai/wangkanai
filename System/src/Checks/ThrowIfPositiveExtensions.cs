// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfPositiveExtensions
{
	public static int ThrowIfPositive([NotNull] this int value)
		=> value.ThrowIfPositive<ArgumentPositiveException>();

	public static int ThrowIfPositive([NotNull] this int value, string message)
		=> value.ThrowIfPositive<ArgumentPositiveException>(message);

	public static int ThrowIfPositive<T>([NotNull] this int value)
		where T : ArgumentException
		=> value.ThrowIfPositive<T>(nameof(value));

	public static int ThrowIfPositive<T>([NotNull] this int value, string message)
		where T : ArgumentException
		=> value > 0
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			   : value;
}
