// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public static class ProjectionComparer
{
	public static ProjectionComparer<TSource, TKey> Create<TSource, TKey>(Func<TSource, TKey> projection)
		=> new(projection);

	public static ProjectionComparer<TSource, TKey> Create<TSource, TKey>(TSource ignored, Func<TSource, TKey> projection)
		=> new(projection);
}

public static class ProjectionComparer<TSource>
{
	public static ProjectionComparer<TSource, TKey> Create<TKey>(Func<TSource, TKey> projection)
		=> new(projection);
}

public class ProjectionComparer<TSource, TKey> : IComparer<TSource>
{
	private readonly Func<TSource, TKey> _projection;
	private readonly IComparer<TKey>     _comparer;

	public ProjectionComparer(Func<TSource, TKey> projection, IComparer<TKey> comparer = null)
	{
		_projection = projection.ThrowIfNull();
		_comparer   = comparer ?? Comparer<TKey>.Default;
	}

	public int Compare(TSource x, TSource y)
		=> (x, y) switch
		{
			(null, null) => 0,
			(null, _)    => -1,
			(_, null)    => 1,
			_            => _comparer.Compare(_projection(x), _projection(y))
		};
}