// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfNullObjectExtensions
{
	public static object ThrowIfNull<T>(this object? value)
		where T : Exception
	{
		return value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value));
	}

	public static object ThrowIfNull<T>(this object? value, string message)
		where T : Exception
	{
		return value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value), message);
	}

	public static T ThrowIfNull<T>(this T value)
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value));
	}

	public static T ThrowIfNull<T>(this T value, string message)
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value), message);
	}
}
