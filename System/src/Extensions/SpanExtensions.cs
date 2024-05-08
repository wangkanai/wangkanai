// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="Span{T}"/> class.
/// </summary>
[DebuggerStepThrough]
public static class SpanExtensions
{
	/// <summary>
	/// Determines if a <see cref="Span{T}"/> is null.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the span.</typeparam>
	/// <param name="span">The span to check.</param>
	/// <returns>True if the span is null; otherwise, false.</returns>
	public static bool IsNull<T>([NotNull] this Span<T> span)
		=> span == null;

	/// <summary>
	/// Determines if a <see cref="Span{T}"/> is empty.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the span.</typeparam>
	/// <param name="span">The span to check.</param>
	/// <returns>True if the span is empty; otherwise, false.</returns>
	public static bool IsEmpty<T>([NotNull] this Span<T> span)
		=> span.Length == 0;

	/// <summary>
	/// Determines if a <see cref="Span{T}"/> is null or empty.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the span.</typeparam>
	/// <param name="span">The span to check.</param>
	/// <returns>True if the span is null or empty; otherwise, false.</returns>
	public static bool IsNullOrEmpty<T>([NotNull] this Span<T> span)
		=> span == null || span.Length == 0;
}
