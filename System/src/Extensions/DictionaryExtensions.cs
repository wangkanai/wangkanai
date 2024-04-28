// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class DictionaryExtensions
{
	[DebuggerStepThrough]
	public static TValue GetValueOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string exceptionMessage)
		=> !dictionary.TryGetValue(key, out var value)
			   ? throw new KeyNotFoundException(exceptionMessage)
			   : value;

	/// <summary>
	/// Doesn't throw an exception when the key is null or does not exist
	/// </summary>
	[DebuggerStepThrough]
	public static TValue GetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		=> dictionary!.GetValueSafe(key, default)!;

	/// <summary>
	/// Doesn't throw an exception when the key is null or does not exist
	/// </summary>
	[DebuggerStepThrough]
	public static TValue GetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		=> key == null || !dictionary.TryGetValue(key, out var value)
			   ? defaultValue
			   : value;

	/// <summary>
	/// Doesn't throw an exception when the key is null
	/// </summary>
	[DebuggerStepThrough]
	public static bool TryGetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
	{
		if (key != null)
			return dictionary.TryGetValue(key, out value!);
		value = default!;
		return false;
	}
}
