// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Collections;

public sealed class RangeIterator<T> : IEnumerable<T>
{
	public Range<T>   Range     { get; }
	public Func<T, T> Step      { get; }
	public bool       Ascending { get; }

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

	public IEnumerator<T> GetEnumerator()
	{
		var includeStart = Ascending ? Range.IncludesStart : Range.IncludesEnd;
		var includeEnd   = Ascending ? Range.IncludesEnd : Range.IncludesStart;

		var start    = Ascending ? Range.Start : Range.End;
		var end      = Ascending ? Range.End : Range.Start;
		var comparer = Ascending ? Range.Comparer : Range.Comparer.Reverse();

		var value = start;
		if (includeStart)
			if (includeEnd || comparer.Compare(value, end) < 0)
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