// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>
/// Default ProjectionComparer helper to produce instances of the generic <see cref="ProjectionComparer{TSource,TKey}"/> class.
/// </summary>
public static class ProjectionComparer
{
	/// <summary>
	/// Creates an instance of the <see cref="ProjectionComparer"/> using the given specified projection.
	/// </summary>
	/// <param name="projection">Projection to use when determining the key of an element</param>
	/// <typeparam name="TSource">Type parameter for the elements to be compared</typeparam>
	/// <typeparam name="TKey">Type parameter for the keys to be compared, after being projected from the elements</typeparam>
	/// <returns>A comparer which will compare elements by projecting each element to its key, and comparing keys</returns>
	public static ProjectionComparer<TSource, TKey> Create<TSource, TKey>(Func<TSource, TKey> projection)
		=> new(projection);

	/// <summary>
	/// Creates an instance of the <see cref="ProjectionComparer"/> using the given specified projection.
	/// </summary>
	/// <param name="ignored">The value is ignored and is solely present to aid type inference</param>
	/// <param name="projection">Projection to use when determining the key of an element</param>
	/// <typeparam name="TSource">Type parameter for the elements to be compared</typeparam>
	/// <typeparam name="TKey">Type parameter for the keys to be compared, after being projected from the elements</typeparam>
	/// <returns>A comparer which will compare elements by projecting each element to its key, and comparing keys</returns>
	public static ProjectionComparer<TSource, TKey> Create<TSource, TKey>(TSource ignored, Func<TSource, TKey> projection)
		=> new(projection);
}

/// <summary>
/// Default generic <see cref="ProjectionComparer{TSource}"/> helper in the source only to produce instances of the double generic <see cref="ProjectionComparer{TSource,TKey}"/> class.
/// Optionally using the type inference.
/// </summary>
public static class ProjectionComparer<TSource>
{
	/// <summary>
	/// Creates an instance of the <see cref="ProjectionComparer{TSource}"/> using the given specified projection.
	/// </summary>
	/// <param name="projection">Projection to use when determining the key of an element</param>
	/// <typeparam name="TKey">Type parameter for the keys to be compared, after being projected from the elements</typeparam>
	/// <returns>A comparer which will compare elements by projecting each element to its key, and comparing keys</returns>
	public static ProjectionComparer<TSource, TKey> Create<TKey>(Func<TSource, TKey> projection)
		=> new(projection);
}

/// <summary>
/// Comparer which projects each element of the comparison to a key, and compares those keys using the specified (or default) comparer for the key type.
/// </summary>
/// <typeparam name="TSource">Type parameter for the elements to be compared</typeparam>
/// <typeparam name="TKey">Type parameter for the keys to be compared, after being projected from the elements</typeparam>
public class ProjectionComparer<TSource, TKey> : IComparer<TSource>
{
	private readonly Func<TSource, TKey> _projection;
	private readonly IComparer<TKey>     _comparer;

	/// <summary>
	/// Creates an instance of the <see cref="ProjectionComparer{TSource,TKey}"/> using the given specified projection, which must not be null.
	/// </summary>
	/// <param name="projection">Projection to use during comparisons</param>
	/// <param name="comparer">The comparer to use on the keys. If null, then that case the default comparer will be used.</param>
	public ProjectionComparer(Func<TSource, TKey> projection, IComparer<TKey>? comparer = null)
	{
		_projection = projection.ThrowIfNull();
		_comparer   = comparer ?? Comparer<TKey>.Default;
	}

	/// <summary>
	/// Compares x and y by projecting them to keys and then comparing the keys.
	/// Null values are not projected, this follow convention of the <see cref="Comparer{T}.Compare(T,T)"/> method.
	/// </summary>
	/// <returns>
	/// Both <see cref="x"/> and <see cref="y"/> are null then return 0;
	/// if <see cref="x"/> is null then return -1;
	/// if <see cref="y"/> is null then return 1;
	/// Otherwise return the standard Compare value.
	/// </returns>
	public int Compare(TSource? x, TSource? y)
		=> (x, y) switch
		   {
			   (null, null) => 0,
			   (null, _)    => -1,
			   (_, null)    => 1,
			   _            => _comparer.Compare(_projection(x), _projection(y))
		   };
}
