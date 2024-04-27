// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[DebuggerStepThrough]
public static class NullConditionalExtensions
{
	[return: NotNull]
	public static bool TrueIfNull<T>([NotNull] this T? value)
		=> value is null;

	[return: NotNull]
	public static bool FalseIfNull<T>([NotNull] this T? value)
		=> value is not null;
}
