// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>
/// Represents a reverse comparer that reverses the ordering of a given comparer.
/// </summary>
/// <typeparam name="T">The type of objects to compare.</typeparam>
public class ReverseComparer<T> : IComparer<T>
{
	/// <summary>
	/// Returns the original comparer; this can be useful to avoid multiple reversals.
	/// </summary>
	public IComparer<T> Original { get; }

	/// <summary>
	/// Create a new reverse comparer
	/// </summary>
	/// <param name="original">The original comparer to use for comparisons</param>
	public ReverseComparer(IComparer<T> original) => Original = original.ThrowIfNull();

	/// <summary>
	/// Compares two objects of type T in reverse order using the original comparer.
	/// </summary>
	/// <param name="x">The first object to compare.</param>
	/// <param name="y">The second object to compare.</param>
	/// <typeparam name="T">The type of the objects to compare.</typeparam>
	/// <returns>
	/// A value that indicates the relative order of the objects being compared.
	/// The return value has the following meanings:
	/// - Less than zero: x is less than y.
	/// - Zero: x equals y.
	/// - Greater than zero: x is greater than y.
	/// </returns>
	public int Compare(T? x, T? y) => Original.Compare(y, x);
}
