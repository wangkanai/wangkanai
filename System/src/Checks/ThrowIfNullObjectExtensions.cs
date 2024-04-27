// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0


namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfNullObjectExtensions
{
	public static object ThrowIfNull<T>([NotNull] this object? value)
		where T : Exception
		=> value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value));

	public static object ThrowIfNull<T>([NotNull] this object? value, string message)
		where T : Exception
		=> value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value), message);
}
