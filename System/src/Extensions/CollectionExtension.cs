// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions;

/// <summary>Class that provides extension method to collection</summary>
[DebuggerStepThrough]
public static class CollectionExtension
{
   /// <summary>Determines whether the collection is null.</summary>
   /// <typeparam name="T">The type of objects within the collection.</typeparam>
   /// <param name="list">The collection to check.</param>
   /// <returns>True if the collection is null; otherwise, false.</returns>
   public static bool IsNull<T>(this ICollection<T>? list)
      => list is null;

   /// <summary>Determines whether the collection is empty.</summary>
   /// <typeparam name="T">The type of objects within the collection.</typeparam>
   /// <param name="list">The collection to check.</param>
   /// <returns>True if the collection is empty or null; otherwise, false.</returns>
   public static bool IsEmpty<T>(this ICollection<T>? list)
      => list is null || list.Count <= 0;

   /// <summary>Determines whether the collection is empty or null.</summary>
   /// <typeparam name="T">The type of objects within the collection.</typeparam>
   /// <param name="list">The collection to check.</param>
   /// <returns>True if the collection is empty or null; otherwise, false.</returns>
   public static bool IsNullOrEmpty<T>(this ICollection<T>? list)
      => list is null || list.Count <= 0;

   /// <summary>Determines whether the given collection is null or empty.</summary>
   /// <typeparam name="T">The type of item in collection.</typeparam>
   /// <param name="list">The collection to check.</param>
   /// <returns>True if the collection is null or empty; otherwise, false.</returns>
   public static bool IsNullOrEmpty<T>(this T[]? list)
      => list is null || list.Length == 0;

   /// <summary>Add a range of items to a collection.</summary>
   /// <param name="list">Type of objects within the collection.</param>
   /// <param name="items">The collection to add items to.</param>
   /// <typeparam name="T">the collection.</typeparam>
   /// <returns>The collection.</returns>
   /// <exception cref="ArgumentNullException">An <see cref="ArgumentNullException"/> is thrown if <paramref name="list"/> or
   /// <paramref name="items"/> is <see langword="null"/>.</exception>
   public static ICollection<T> AddRangeSafe<T>(this ICollection<T> list, IEnumerable<T> items)
   {
      list.ThrowIfNull();
      items.ThrowIfNull();

      foreach (var each in items)
         list.Add(each);

      return list;
   }

   /// <summary>Adds distinct items to the collection.</summary>
   /// <typeparam name="T">The type of objects within the collection.</typeparam>
   /// <param name="list">The collection to add items to.</param>
   /// <param name="items">The items to add.</param>
   /// <returns>The modified collection with the distinct items added.</returns>
   /// <exception cref="ArgumentNullException">Thrown when <paramref name="list"/> or <paramref name="items"/> is null.</exception>
   public static ICollection<T> AddDistinct<T>(this ICollection<T> list, params T[] items)
      => AddDistinct(list, null!, items);

   /// <summary>Adds distinct items to the collection.</summary>
   /// <param name="list">The collection to add the items to.</param>
   /// <param name="comparer">An optional comparer to use for comparing items.</param>
   /// <param name="items">The items to add to the collection.</param>
   /// <typeparam name="T">The type of items in the collection.</typeparam>
   /// <returns>The collection with the distinct items added.</returns>
   public static ICollection<T> AddDistinct<T>(this ICollection<T> list, IEqualityComparer<T> comparer, params T[] items)
   {
      list.ThrowIfNull()
          .ThrowIfEmpty();
      items.ThrowIfNull()
           .ThrowIfEmpty();

      foreach (var item in items)
      {
         var contains = comparer != null ? list.Contains(item, comparer) : list.Contains(item);
         if (contains)
            continue;
         list.Add(item);
      }

      return list;
   }

   /// <summary>Replaces the items in the collection with the specified items.</summary>
   /// <typeparam name="T">The type of objects within the collection.</typeparam>
   /// <param name="list">The collection to replace items in.</param>
   /// <param name="items">The items to replace the collection with.</param>
   /// <returns>The modified collection with the specified items.</returns>
   public static ICollection<T> Replace<T>(this ICollection<T> list, IEnumerable<T> items)
   {
      list.ThrowIfNull().ThrowIfEmpty();
      items.ThrowIfNull().ThrowIfEmpty();

      list.Clear();
      list.AddRangeSafe(items);

      return list;
   }
}