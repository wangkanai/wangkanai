// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Net.Http.Headers;

namespace Wangkanai.Extensions;

public static class DictionaryExtension
{
	public static TValue GetValueOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string exceptionMessage)
	{
		return !dictionary.TryGetValue(key, out var value)
			       ? throw new KeyNotFoundException(exceptionMessage)
			       : value;
	}

	/// <summary>
	/// Doesn't throw an exception when the key is null or does not exist
	/// </summary>
	public static TValue GetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
	{
		return dictionary.GetValueSafe(key, default);
	}

	/// <summary>
	/// Doesn't throw an exception when the key is null or does not exist
	/// </summary>
	public static TValue GetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
	{
		return key != null && dictionary.TryGetValue(key, out var value)
			       ? value
			       : defaultValue;
	}

	/// <summary>
	/// Doesn't throw an exception when the key is null
	/// </summary>
	public static bool TryGetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
	{
		if (key != null)
			return dictionary.TryGetValue(key, out value);

		value = default;

		return false;
	}
}