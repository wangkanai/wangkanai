// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>
/// Comparer to daisy-chain multiple comparers and apply them in order.
/// </summary>
internal class LinkedComparer<T> : IComparer<T>
{
	private readonly IComparer<T> _primary;
	private readonly IComparer<T> _secondary;

	/// <summary>
	/// Create a new LinkedComparer.
	/// </summary>
	/// <param name="primary">The first comparison to use</param>
	/// <param name="secondary">The next level of comparison if the primary return 0 (equivalent)</param>
	public LinkedComparer(IComparer<T> primary, IComparer<T> secondary)
	{
		_primary   = primary.ThrowIfNull();
		_secondary = secondary.ThrowIfNull();
	}

	/// <summary>
	/// Comparer to daisy chain multiple comparers and apply them in order.
	/// </summary>
	/// <typeparam name="T">The type of objects being compared.</typeparam>
	public int Compare(T? x, T? y)
	{
		var result = _primary.Compare(x, y);
		return result == 0
			       ? _secondary.Compare(x, y)
			       : result;
	}
}
