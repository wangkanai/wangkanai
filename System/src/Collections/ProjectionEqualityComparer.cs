// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public static class ProjectionEqualityComparer
{
	public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(Func<TSource, TKey> projection)
		=> new(projection);

	public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(TSource ignored, Func<TSource, TKey> projection)
		=> new(projection);
}

public static class ProjectionEqualityComparer<TSource>
{
	public static ProjectionEqualityComparer<TSource, TKey> Create<TKey>(Func<TSource, TKey> projection)
		=> new(projection);
}

public class ProjectionEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
{
	private readonly Func<TSource, TKey>     _projection;
	private readonly IEqualityComparer<TKey> _comparer;

	public ProjectionEqualityComparer(Func<TSource, TKey> projection, IEqualityComparer<TKey> comparer = null)
	{
		_projection = projection.ThrowIfNull();
		_comparer   = comparer ?? EqualityComparer<TKey>.Default;
	}

	public bool Equals(TSource x, TSource y)
		=> (x, y) switch
		{
			(null, null) => true,
			(null, _)    => false,
			(_, null)    => false,
			_            => _comparer.Equals(_projection(x), _projection(y))
		};

	public int GetHashCode(TSource obj)
		=> _comparer.GetHashCode(_projection(obj.ThrowIfNull()));
}