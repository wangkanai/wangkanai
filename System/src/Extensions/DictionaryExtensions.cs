// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

/// <summary>
/// Provides extension methods for dictionaries.
/// </summary>
[DebuggerStepThrough]
public static class DictionaryExtensions
{
	/// <summary>Returns the value associated with the specified key from the dictionary or throws a KeyNotFoundException with the specified exception message if the key does not exist.</summary>
	/// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to retrieve the value from.</param>
	/// <param name="key">The key to retrieve the value for.</param>
	/// <param name="exceptionMessage">The exception message to be used in the KeyNotFoundException.</param>
	/// <returns>The value associated with the key.</returns>
	/// <exception cref="KeyNotFoundException">Thrown when the specified key does not exist in the dictionary.</exception>
	public static TValue GetValueOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string exceptionMessage)
		=> !dictionary.TryGetValue(key, out var value)
			   ? throw new KeyNotFoundException(exceptionMessage)
			   : value;

	/// <summary>Doesn't throw an exception when the key is null or does not exist. Returns the value associated with the specified key from the dictionary, or a default value if the key is null or does not exist. </summary>
	/// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to retrieve the value from.</param>
	/// <param name="key">The key to retrieve the value for.</param>
	/// <returns>The value associated with the key, or the default value if the key is null or does not exist.</returns>
	public static TValue GetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		=> dictionary!.GetValueSafe(key, default)!;

	/// <summary>Doesn't throw an exception when the key is null or does not exist. Returns the value associated with the specified key from the dictionary, or a default value if the key is null or does not exist.</summary>
	/// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to retrieve the value from.</param>
	/// <param name="key">The key to retrieve the value for.</param>
	/// <param name="defaultValue">The default value to be returned if the key is null or does not exist.</param>
	/// <returns>The value associated with the key, or the default value if the key is null or does not exist.</returns>
	public static TValue GetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		=> key.EqualNull() || !dictionary.TryGetValue(key, out var value)
			   ? defaultValue
			   : value;

	/// <summary>Returns the value associated with the specified key from the dictionary, allowing for safe retrieval without throwing a KeyNotFoundException if the key does not exist.</summary>
	/// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to retrieve the value from.</param>
	/// <param name="key">The key to retrieve the value for.</param>
	/// <param name="value">When this method returns, contains the value associated with the specified key if the key is found; otherwise, the default value for the type of the value parameter.</param>
	/// <returns>true if the dictionary contains an element with the specified key; otherwise, false.</returns>
	public static bool TryGetValueSafe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
	{
		if (key.NotNull())
			return dictionary.TryGetValue(key, out value!);
		value = default!;
		return false;
	}
}
