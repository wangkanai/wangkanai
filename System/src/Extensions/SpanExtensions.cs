// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class SpanExtensions
{
	public static bool IsNull<T>([NotNull] this Span<T> span)
		=> span == null;

	public static bool IsEmpty<T>([NotNull] this Span<T> span)
		=> span.Length == 0;

	public static bool IsNullOrEmpty<T>([NotNull] this Span<T> span)
		=> span == null || span.Length == 0;
}
