// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public sealed class ReverseComparer<T> : IComparer<T>
{
	/// <summary>
	/// Returns the original comparer; this can be useful to avoid multiple reversals.
	/// </summary>
	public IComparer<T> Original { get; }

	/// <summary>
	/// Create a new reverse comparer
	/// </summary>
	/// <param name="original">The original comparer to use for comparisons</param>
	public ReverseComparer(IComparer<T> original)
	{
		original.ThrowIfNull();

		Original = original;
	}

	/// <summary>
	/// Returns the result of comparing the specified values using the original comparer, but reversing the order of comparison
	/// </summary>
	/// <returns></returns>
	public int Compare(T x, T y) => Original.Compare(y, x);
}