// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Caching;

/// <summary>Provides functionality to generate custom cache keys for storing and retrieving cached data.</summary>
public static class CacheKey
{
   /// <summary>Generates a cache key by concatenating the provided keys with a "-" separator.</summary>
   /// <param name="keys">An array of strings to combine into a cache key.</param>
   /// <returns>A concatenated string key created by combining the input strings separated by "-".</returns>
   public static string With(params string[] keys)
      => string.Join("-", keys);

   /// <summary>Generates a cache key by combining a type key and the provided keys with a "-" separator.</summary>
   /// <param name="type">The type for which the cache key is being generated.</param>
   /// <param name="keys">An array of strings to be appended to the type-derived key.</param>
   /// <returns>A formatted cache key string consisting of the type key and the input keys separated by "-".</returns>
   public static string With(Type type, params string[] keys)
      => With($"{type.GetCacheKey()}:{string.Join("-", keys)}");
}