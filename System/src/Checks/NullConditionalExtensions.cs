// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#pragma warning disable CS8777 // Parameter must have a non-null value when exiting.

namespace Wangkanai;

[DebuggerStepThrough]
public static class NullConditionalExtensions
{
	public static bool TrueIfNull<T>([NotNull] this T value)
		=> value is null;

	public static bool FalseIfNull<T>([NotNull] this T value)
		=> value is not null;
}
