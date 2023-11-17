// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfZeroExtensions
{
	public static int ThrowIfZero(this int value)
	{
		return value.ThrowIfZero<ArgumentZeroException>();
	}

	public static int ThrowIfZero(this int value, string message)
	{
		return value.ThrowIfZero<ArgumentZeroException>(message);
	}

	public static int ThrowIfZero<T>(this int value)
		where T : ArgumentException
	{
		return value == 0
			       ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value))
			       : value;
	}

	public static int ThrowIfZero<T>(this int value, string message)
		where T : ArgumentException
	{
		return value == 0
			       ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			       : value;
	}
}
