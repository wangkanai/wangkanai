// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Collections;

/// <summary>
/// Iterates over a range of values that <see cref="IEnumerable{T}"/>
///  </summary>
public sealed class RangeIterator<T> : IEnumerable<T>
{
	/// <summary>
	/// Returns the range this object iterates over.
	/// </summary>
	public Range<T> Range { get; }

	/// <summary>
	/// Returns the step function used for this range
	/// </summary>
	public Func<T, T> Step { get; }

	/// <summary>
	/// Returns whether or not this iterator works up from the start point (ascending) or down from the end point (descending)
	/// </summary>
	public bool Ascending { get; }

	/// <summary>
	/// Create an ascending iterator iver the given range with the given step function, with the specified direction (optional)
	/// </summary>
	public RangeIterator(Range<T> range, Func<T, T> step, bool ascending = true)
	{
		step.ThrowIfNull();

		if (ascending && range.Comparer.Compare(range.Start, step(range.Start)) >= 0 ||
		    !ascending && range.Comparer.Compare(range.End, step(range.End)) <= 0)
			throw new ArgumentException("Step does nothing, or progresses the wrong way");

		Range     = range;
		Step      = step;
		Ascending = ascending;
	}

	/// <summary>
	/// Returns a <see cref="IEnumerator{T}"/> running over the range.
	/// </summary>
	public IEnumerator<T> GetEnumerator()
	{
		var includeStart = Ascending ? Range.IncludesStart : Range.IncludesEnd;
		var includeEnd   = Ascending ? Range.IncludesEnd : Range.IncludesStart;

		var start    = Ascending ? Range.Start : Range.End;
		var end      = Ascending ? Range.End : Range.Start;
		var comparer = Ascending ? Range.Comparer : Range.Comparer.Reverse();

		var value = start;
		// In case that start point == end point
		if (includeStart && (includeEnd || comparer.Compare(value, end) < 0))
			yield return value;

		value = Step(value);

		while (comparer.Compare(value, end) < 0)
		{
			yield return value;
			value = Step(value);
		}

		if (includeEnd && comparer.Compare(value, end) == 0)
			yield return value;
	}

	/// <summary>
	/// Returns on <see cref="IEnumerator"/> running over the range
	/// </summary>
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
