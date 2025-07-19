// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>
/// Provides a string array comparer for comparing string arrays for equality.
/// </summary>
internal sealed class StringArrayComparer : IEqualityComparer<string[]>
{
	public static readonly StringArrayComparer Ordinal = new(StringComparer.Ordinal);
	public static readonly StringArrayComparer OrdinalIgnoreCase = new(StringComparer.OrdinalIgnoreCase);

	private readonly StringComparer _valueComparer;

	/// <summary>
	/// Provides a string array comparer for comparing string arrays for equality.
	/// </summary>
	public StringArrayComparer(StringComparer valueComparer)
	{
		_valueComparer = valueComparer;
	}

	public bool Equals(string[]? x, string[]? y)
	{
		if (ReferenceEquals(x, y))
			return true;

		if (x == null && y == null)
			return true;

		if (x == null || y == null)
			return false;

		if (x.Length != y.Length)
			return false;

		for (var i = 0; i < x.Length; i++)
		{
			if (string.IsNullOrEmpty(x[i]) && string.IsNullOrEmpty(y[i]))
				continue;

			if (!_valueComparer.Equals(x[i], y[i]))
				return false;
		}

		return true;
	}

	/// <summary>
	/// Calculates the hash code for a string array based on its elements using the provided comparer.
	/// </summary>
	/// <param name="obj">The string array to calculate the hash code for.</param>
	/// <returns>The hash code for the input string array.</returns>
	public int GetHashCode(string[] obj)
	{
		if (obj == null)
			return 0;

		var hash = new HashCode();
		for (var i = 0; i < obj.Length; i++)
			hash.Add(obj[i] ?? string.Empty, _valueComparer);

		return hash.ToHashCode();
	}
}
