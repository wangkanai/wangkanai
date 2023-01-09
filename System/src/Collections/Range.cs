// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public sealed class Range<T>
{
	public T            Start        { get; }
	public T            End          { get; }
	public IComparer<T> Comparer     { get; }
	public bool         IncludeStart { get; }
	public bool         IncludeEnd   { get; }
	public Range(T start, T end) : this(start, end, Comparer<T>.Default) { }

	public Range(T start, T end, IComparer<T> comparer, bool includeStart = true, bool includeEnd = true)
	{
		if (comparer.Compare(start, end) > 0)
			throw new ArgumentOutOfRangeException(nameof(end), "start must be lower than end according to comparer");

		Start        = start;
		End          = end;
		Comparer     = comparer;
		IncludeStart = includeStart;
		IncludeEnd   = includeEnd;
	}

	public Range<T> ExcludeEnd()
	{
		if (!IncludeEnd)
			return this;

		return new Range<T>(Start, End, Comparer, false, IncludeEnd);
	}

	public Range<T> ExcludeStart()
	{
		if (!IncludeEnd)
			return this;

		return new Range<T>(Start, End, Comparer, IncludeStart, true);
	}

	public bool Contains(T value)
	{
		var lower = Comparer.Compare(value, Start);
		if (lower < 0 || lower == 0 && !IncludeStart)
			return false;

		var upper = Comparer.Compare(value, End);

		return upper < 0 || upper == 0 && IncludeEnd;
	}
}

public class RangeIterator<T> : IEnumerable<T>
{
	public IEnumerator<T> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Returns on <see cref="IEnumerator"/> running over the range
	/// </summary>
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}