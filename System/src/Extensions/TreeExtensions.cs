// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions;

/// <summary>Provides extension methods for tree operations on a generic type.</summary>
[DebuggerStepThrough]
public static class TreeExtensions
{
   /// <summary>Traverses a tree structure starting from a specified node and retrieves all nodes in a depth-first manner.</summary>
   /// <typeparam name="T">The type of the tree node.</typeparam>
   /// <param name="node">The starting node.</param>
   /// <param name="children">A function that retrieves the children nodes of a given node.</param>
   /// <returns>An enumerable collection of all nodes in the tree structure.</returns>
   public static IEnumerable<T> Traverse<T>(this T node, Func<T, IEnumerable<T>> children)
   {
      yield return node;

      var childNodes = children(node);
      if (children.TrueIfNull())
      {
         yield break;
      }

      foreach (var child in childNodes.SelectMany(n => n.Traverse(children)))
         yield return child;
   }

   /// <summary>Retrieves all ancestors of the specified item in a tree structure.</summary>
   /// <typeparam name="T">The type of the tree node.</typeparam>
   /// <param name="item">The item to get the ancestors for.</param>
   /// <param name="getParentFunc">A function that retrieves the parent node of a given node.</param>
   /// <returns>An enumerable collection of the ancestors of the item.</returns>
   public static IEnumerable<T> GetAncestors<T>(this T item, Func<T, T> getParentFunc)
   {
      getParentFunc.ThrowIfNull();

      if (ReferenceEquals(item, null))
      {
         yield break;
      }

      for (var curItem = getParentFunc(item); !ReferenceEquals(curItem, null); curItem = getParentFunc(curItem))
         yield return curItem;
   }
}