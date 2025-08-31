// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Caching;

/// <summary>An interface that defines the structure for creating a cache key.</summary>
/// <remarks>Provides a mechanism to generate a unique string identifier representing the current state or identity of a class implementing this interface. This can be utilized in caching strategies to efficiently store and retrieve data based on the derived cache key.</remarks>
public interface ICacheKey
{
   /// <summary>Generates a unique cache key representing the state or identity of the current object.</summary>
   /// <returns>A string that serves as a cache key for the current object instance.</returns>
   string GetCacheKey();
}