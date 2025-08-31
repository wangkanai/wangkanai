// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Collections;
using System.Linq.Expressions;

using Wangkanai.Domain.Common;

namespace Wangkanai.Domain.Tests.Common;

public class ReflectionUtilityTests
{
   [Fact]
   public void GetPropertyName_WithValidExpression_ReturnsPropertyName()
   {
      // Arrange
      Expression<Func<TestClass, object>> expression = x => x.Name;

      // Act
      var result = ReflectionUtility.GetPropertyName(expression);

      // Assert
      Assert.Equal("Name", result);
   }

   [Fact]
   public void GetPropertyName_WithValueTypeProperty_ReturnsPropertyName()
   {
      // Arrange
      Expression<Func<TestClass, object>> expression = x => x.Id;

      // Act
      var result = ReflectionUtility.GetPropertyName(expression);

      // Assert
      Assert.Equal("Id", result);
   }

   [Fact]
   public void GetPropertyName_WithNullExpression_ReturnsNull()
   {
      // Arrange
      Expression<Func<TestClass, object>>? expression = null;

      // Act
      var result = ReflectionUtility.GetPropertyName(expression);

      // Assert
      Assert.Null(result);
   }

   [Fact]
   public void GetPropertyNames_WithMultipleExpressions_ReturnsAllPropertyNames()
   {
      // Arrange
      Expression<Func<TestClass, object>> expression1 = x => x.Id;
      Expression<Func<TestClass, object>> expression2 = x => x.Name;
      Expression<Func<TestClass, object>> expression3 = x => x.CreatedDate;

      // Act
      var result = ReflectionUtility.GetPropertyNames(expression1, expression2, expression3);

      // Assert
      Assert.Equal(3, result.Count());
      Assert.Contains("Id",          result);
      Assert.Contains("Name",        result);
      Assert.Contains("CreatedDate", result);
   }

   [Fact]
   public void GetPropertyNames_WithNoExpressions_ReturnsEmptyCollection()
   {
      // Act
      var result = ReflectionUtility.GetPropertyNames<TestClass>();

      // Assert
      Assert.Empty(result);
   }

   [Fact]
   public void IsAssignableFromGenericList_WithGenericListType_ReturnsTrue()
   {
      // Arrange
      var listType = typeof(List<string>);

      // Act
      var result = listType.IsAssignableFromGenericList();

      // Assert
      Assert.True(result);
   }

   [Fact]
   public void IsAssignableFromGenericList_WithArrayType_ReturnsFalse()
   {
      // Arrange
      var arrayType = typeof(string[]);

      // Act
      var result = arrayType.IsAssignableFromGenericList();

      // Assert
      Assert.True(result);
   }

   [Fact]
   public void IsAssignableFromGenericList_WithNonCollectionType_ReturnsFalse()
   {
      // Arrange
      var stringType = typeof(string);

      // Act
      var result = stringType.IsAssignableFromGenericList();

      // Assert
      Assert.False(result);
   }

   [Fact]
   public void IsAssignableFromGenericList_WithCustomCollectionImplementingIList_ReturnsTrue()
   {
      // Arrange
      var customListType = typeof(CustomList<int>);

      // Act
      var result = customListType.IsAssignableFromGenericList();

      // Assert
      Assert.True(result);
   }

   private class TestClass
   {
      public int Id { get; set; }

      public string Name
      {
         get;
      } = string.Empty;

      public DateTime CreatedDate { get; set; }
   }

   private class CustomList<T> : IList<T>
   {
      private readonly List<T> _list = new();

      public IEnumerator<T>   GetEnumerator()                   => _list.GetEnumerator();
      IEnumerator IEnumerable.GetEnumerator()                   => GetEnumerator();
      public void             Add(T item)                       => _list.Add(item);
      public void             Clear()                           => _list.Clear();
      public bool             Contains(T item)                  => _list.Contains(item);
      public void             CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
      public bool             Remove(T   item)            => _list.Remove(item);
      public int              Count                       => _list.Count;
      public bool             IsReadOnly                  => false;
      public int              IndexOf(T    item)          => _list.IndexOf(item);
      public void             Insert(int   index, T item) => _list.Insert(index, item);
      public void             RemoveAt(int index) => _list.RemoveAt(index);

      public T this[int index]
      {
         get => _list[index];
         set => _list[index] = value;
      }
   }
}