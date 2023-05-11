// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.System.Collections;

/// <summary>
/// Default ProjectionEqualityComparer helper to produce instances of the generic <see cref="ProjectionEqualityComparer{TSource,TKey}"/> class.
/// </summary>
public static class ProjectionEqualityComparer
{
	public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(Func<TSource, TKey> projection)
		=> new(projection);

	public static ProjectionEqualityComparer<TSource, TKey> Create<TSource, TKey>(TSource ignored, Func<TSource, TKey> projection)
		=> new(projection);
}

/// <summary>
/// Default generic <see cref="ProjectionEqualityComparer{TSource}"/> helper in the source only to produce instances of the double generic <see cref="ProjectionEqualityComparer{TSource,TKey}"/> class.
/// Optionally using the type inference.
/// </summary>
public static class ProjectionEqualityComparer<TSource>
{
	public static ProjectionEqualityComparer<TSource, TKey> Create<TKey>(Func<TSource, TKey> projection)
		=> new(projection);
}

/// <summary>
/// Comparer which projects each element of the comparison to a key, and compares those keys using the specified (or default) comparer for the key type.
/// </summary>
/// <typeparam name="TSource">Type parameter for the elements to be compared</typeparam>
/// <typeparam name="TKey">Type parameter for the keys to be compared, after being projected from the elements</typeparam>
public class ProjectionEqualityComparer<TSource, TKey> : IEqualityComparer<TSource>
{
	private readonly Func<TSource, TKey>     _projection;
	private readonly IEqualityComparer<TKey> _comparer;

	/// <summary>
	/// Creates an instance of the <see cref="ProjectionEqualityComparer{TSource,TKey}"/> using the given specified projection, which must not be null.
	/// </summary>
	/// <param name="projection">Projection to use during comparisons</param>
	/// <param name="comparer">The comparer to use on the keys. If null, then that case the default comparer will be used.</param>
	public ProjectionEqualityComparer(Func<TSource, TKey> projection, IEqualityComparer<TKey> comparer = null)
	{
		_projection = projection.ThrowIfNull();
		_comparer   = comparer ?? EqualityComparer<TKey>.Default;
	}

	/// <summary>
	/// Compares x and y for equality by applying the projection to each value and then using the equality comparer on the resulting keys.
	/// </summary>
	/// <returns>
	/// Both <see cref="x"/> and <see cref="y"/> are null then return True;
	/// If only one of <see cref="x"/> and <see cref="y"/> is null then return False;
	/// Otherwise return the standard Compare value.
	/// </returns>
	public bool Equals(TSource x, TSource y)
		=> (x, y) switch
		{
			(null, null) => true,
			(null, _)    => false,
			(_, null)    => false,
			_            => _comparer.Equals(_projection(x), _projection(y))
		};

	/// <summary>
	/// Produces a hash code for the given value by projecting it and then asking the equality comparer for the hash code of the resulting key.
	/// </summary>
	public int GetHashCode(TSource obj)
		=> _comparer.GetHashCode(_projection(obj.ThrowIfNull()));
}