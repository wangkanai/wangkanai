// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>
/// Types mapping to instance of those types.
/// </summary>
public sealed class DictionaryByType
{
	private readonly Dictionary<Type, object> _dictionary = new();

	/// <summary>
	/// Maps the specified type to the specified instance of the given value.
	/// If the type argument already exists within the dictionary, throw ArgumentException.
	/// </summary>
	public void Add<T>(T value)
		=> _dictionary.Add(typeof(T), value!);

	/// <summary>
	/// Maps the specified type to the specified instance of the given value.
	/// If the type argument already exists within the dictionary, then it will be overwritten.
	/// </summary>
	public void Put<T>(T value)
		=> _dictionary[typeof(T)] = value!;

	/// <summary>
	/// Try to fetch a value from the dictionary.
	/// If the type argument does not exist within the dictionary, throw a KeyNotFoundException.
	/// </summary>
	public T Get<T>()
		=> (T)_dictionary[typeof(T)];

	/// <summary>
	/// Try to get the instance of the given type from the dictionary, returning true if the type exists.
	/// Otherwise, return false and setting the output parameter to the default value for <see cref="T"/> if it fails.
	/// </summary>
	public bool TryGet<T>(out T value)
	{
		if (_dictionary.TryGetValue(typeof(T), out var temp))
		{
			value = (T)temp;
			return true;
		}

		value = default!;
		return false;
	}
}
