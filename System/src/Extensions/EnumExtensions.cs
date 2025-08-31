// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel;

namespace Wangkanai.Extensions;

/// <summary>Provides extension methods for working with enumerations.</summary>
[DebuggerStepThrough]
public static class EnumExtensions
{
   /// <summary>Converts the specified enumeration value to its original string representation.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The enumeration value to convert.</param>
   /// <returns>The original string representation of the enumeration value.</returns>
   public static string ToOriginalString<T>(this T value)
      where T : Enum
      => EnumValues<T>.GetNameOriginal(value);

   /// <summary>Converts the specified enumeration value to its lowercased string representation.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The enumeration value to convert.</param>
   /// <returns>The lowercased string representation of the enumeration value.</returns>
   public static string ToLowerString<T>(this T value)
      where T : Enum
      => EnumValues<T>.GetNameOriginal(value).ToLowerInvariant();

   /// <summary>Converts the specified enumeration value to its uppercased string representation.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The enumeration value to convert.</param>
   /// <returns>The uppercased string representation of the enumeration value.</returns>
   public static string ToUpperString<T>(this T value)
      where T : Enum
      => EnumValues<T>.GetNameOriginal(value).ToUpperInvariant();

   /// <summary>Determines whether the specified string contains the original string representation of the specified enumeration value.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The string value to search within.</param>
   /// <param name="flags">The enumeration value to search for.</param>
   /// <returns><c>true</c> if the string contains the original string representation of the enumeration value; otherwise, <c>false</c>.</returns>
   public static bool ContainsOriginal<T>(this string value, T flags)
      where T : Enum
      => value.ContainSingle(flags) ||
         flags.GetFlags().Any(item => value.Contains(item.ToOriginalString(), StringComparison.Ordinal));

   /// <summary>Determines whether the specified string value contains an uppercase representation of the specified flags.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The string value to check.</param>
   /// <param name="flags">The flags to search for in the uppercase representation.</param>
   /// <returns>True if the string value contains an uppercase representation of the flags; otherwise, false.</returns>
   public static bool ContainsUpper<T>(this string value, T flags)
      where T : Enum
      => value.ContainSingle(flags) ||
         flags.GetFlags().Any(item => value.Contains(item.ToUpperString(), StringComparison.Ordinal));

   /// <summary>Determines whether the specified string contains the lowercased string representation of the given enumeration value.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The string value to search within.</param>
   /// <param name="flags">The enumeration value to search for.</param>
   /// <returns>true if the lowercased string representation of the enumeration value is found within the string; otherwise, false.</returns>
   public static bool ContainsLower<T>(this string value, T flags)
      where T : Enum
      => value.ContainSingle(flags) ||
         flags.GetFlags().Any(item => value.Contains(item.ToLowerString(), StringComparison.Ordinal));

   /// <summary>Determines whether the specified string contains the original string representation of the specified enumeration value.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The string value to search within.</param>
   /// <param name="flags">The enumeration value to search for.</param>
   /// <returns><c>true</c> if the string contains the original string representation of the enumeration value; otherwise, <c>false</c>.</returns>
   private static bool ContainSingle<T>(this string value, T flags)
      where T : Enum
      => EnumValues<T>.TryGetSingleName(flags, out var name) &&
         value.Contains(name, StringComparison.Ordinal);

   /// <summary>Retrieves the individual flags of an enumeration value.</summary>
   /// <typeparam name="T">The type of the enumeration.</typeparam>
   /// <param name="value">The enumeration value to retrieve the flags from.</param>
   /// <returns>An enumerable collection of the individual flags of the enumeration value.</returns>
   public static IReadOnlySet<T> GetFlags<T>(this T value)
      where T : Enum
      => EnumValues<T>.GetFlags(value);

   /// <summary>Gets the description attributed to an enumeration value.</summary>
   /// <param name="value">The enumeration value.</param>
   /// <returns>The description attributed to the enumeration value, or the string representation of the value if no description is found.</returns>
   public static string GetDescription(this Enum value)
   {
      var type   = value.GetType();
      var member = type.GetMember(value.ToString());
      if (member.Length <= 0)
      {
         return value.ToString();
      }

      var attributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
      if (attributes.IsEmpty())
      {
         return value.ToString();
      }

      return attributes.OfType<DescriptionAttribute>()
                       .SingleOrDefault()
                      ?.Description ?? string.Empty;
   }

   /// <summary>Gets the custom value of the specified enumeration member, if available.</summary>
   /// <param name="value">The enumeration member.</param>
   /// <returns>The custom value of the enumeration member, or an empty string if not available.</returns>
   public static string GetMemberValue(this Enum value)
   {
      var field = value.GetType().GetField(value.ToString());

      if (field is null)
      {
         return string.Empty;
      }

      var attributes = field.GetCustomAttributes(typeof(EnumMemberAttribute), false);
      if (attributes.IsEmpty())
      {
         return value.ToString();
      }

      return attributes.OfType<EnumMemberAttribute>().SingleOrDefault()?.Value ?? string.Empty;
   }
}