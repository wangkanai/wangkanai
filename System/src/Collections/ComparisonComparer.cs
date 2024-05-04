// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>
///	Delegate comparison implementing generic of <see cref="IComparer{T}"/> and method extensions to perform reverse operation.
/// </summary>
public sealed class ComparisonComparer<T>(Comparison<T> comparison) : IComparer<T>
{
	private readonly Comparison<T> _comparison = comparison.ThrowIfNull();

	/// <summary>
	/// Compares two nullable values of type T.
	/// </summary>
	/// <param name="x">The first value to compare.</param>
	/// <param name="y">The second value to compare.</param>
	/// <returns>
	/// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>.
	/// The return value has the following meanings:
	/// - Less than 0: <paramref name="x"/> is less than <paramref name="y"/>.
	/// - 0: <paramref name="x"/> is equal to <paramref name="y"/>.
	/// - Greater than 0: <paramref name="x"/> is greater than <paramref name="y"/>.
	/// </returns>
	public int Compare(T? x, T? y) => _comparison(x!, y!);

	/// <summary>
	/// Creates a comparison delegate from an <see cref="IComparer{T}"/> instance.
	/// </summary>
	/// <typeparam name="T">The type of the values to compare.</typeparam>
	/// <param name="comparer">The <see cref="IComparer{T}"/> instance to create the comparison from.</param>
	/// <returns>A comparison delegate that compares two nullable values of type T.</returns>
	public static Comparison<T> CreateComparison(IComparer<T> comparer) => comparer.ThrowIfNull().Compare;
}
