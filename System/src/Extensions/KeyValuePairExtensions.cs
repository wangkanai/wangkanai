// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class KeyValuePairExtensions
{
	/// <summary>
	/// Withes the key
	/// </summary>
	/// <param name="keyvalue">The KeyValuePair</param>
	/// <param name="newKey">The new key</param>
	/// <typeparam name="TKey">The type of the TKey</typeparam>
	/// <typeparam name="TValue">The type of the TValue</typeparam>
	/// <returns>KeyValuePair{``0``1}</returns>
	[DebuggerStepThrough]
	public static KeyValuePair<TKey, TValue> WithKey<TKey, TValue>(this KeyValuePair<TKey, TValue> keyvalue, TKey newKey)
		=> new(newKey, keyvalue.Value);

	/// <summary>
	/// Withes the key
	/// </summary>
	/// <param name="keyvalue">The KeyValuePair</param>
	/// <param name="newValue">The new value</param>
	/// <typeparam name="TKey">The type of the TKey</typeparam>
	/// <typeparam name="TValue">The type of the TValue</typeparam>
	/// <returns>KeyValuePair{``0``1}</returns>
	[DebuggerStepThrough]
	public static KeyValuePair<TKey, TValue> WithValue<TKey, TValue>(this KeyValuePair<TKey, TValue> keyvalue, TValue newValue)
		=> new(keyvalue.Key, newValue);
}
