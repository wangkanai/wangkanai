// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfNullGenericExtensions
{
	public static T ThrowIfNull<T>([NotNull] this T value)
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value));

	public static T ThrowIfNull<T>([NotNull] this T value, string message)
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value), message);
}
