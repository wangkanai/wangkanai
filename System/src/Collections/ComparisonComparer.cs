// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>
///	Delegate comparison implementing generic of <see cref="IComparer{T}"/> and method extensions to perform reverse operation.
/// </summary>
public sealed class ComparisonComparer<T>(Comparison<T> comparison) : IComparer<T>
{
	private readonly Comparison<T> _comparison = ThrowIfNullObjectExtensions.ThrowIfNull<Comparison<T>>(comparison);

	public int Compare(T? x, T? y)
		=> _comparison(x!, y!);

	public static Comparison<T> CreateComparison(IComparer<T> comparer)
		=> comparer.ThrowIfNull().Compare;
}
