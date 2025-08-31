// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions;

/// <summary>Contains extension methods for the <see cref="string"/> class.</summary>
[DebuggerStepThrough]
public static class StringExtensions
{
   // Special characters set for EscapeSearch - defined at class level for reuse
   private static readonly HashSet<char> SpecialCharacters = new() { '+', '-', '!', '(', ')', '{', '}', '[', ']', '^', '"', '~', '*', '?', ':', '\\' };

   // Static regex patterns for GenerateSlug - defined at class level for reuse
   private static readonly Regex InvalidCharsRegex   = new(@"[^a-z0-9\s-]", RegexOptions.Compiled);
   private static readonly Regex MultipleSpacesRegex = new(@"\s+", RegexOptions.Compiled);
   private static readonly Regex SpaceToHyphenRegex  = new(@"\s", RegexOptions.Compiled);

   private static readonly char[] Separator = [' '];

   /// <summary>Determines whether the given string is empty.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string is empty; otherwise, false.</returns>
   public static bool IsEmpty(this string value)
      => value == string.Empty;

   /// <summary>Determines whether the given string is null or empty.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string is null or empty; otherwise, false.</returns>
   public static bool IsNullOrEmpty(this string? value)
      => string.IsNullOrEmpty(value);

   /// <summary>Determines whether the given string is not null or empty.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string is not null or empty; otherwise, false.</returns>
   public static bool IsNotNullOrEmpty(this string? value)
      => !string.IsNullOrEmpty(value);

   /// <summary>Determines whether the given string is null, empty, or consists only of white space characters.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string is null, empty, or consists only of white space characters; otherwise, false.</returns>
   public static bool IsNullOrWhiteSpace(this string? value)
      => string.IsNullOrWhiteSpace(value);

   /// <summary>Determines whether the given string consists of only white space characters.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string consists of only white space characters; otherwise, false.</returns>
   public static bool IsWhiteSpace(this string value)
      => !value.IsNullOrEmpty() && value.All(char.IsWhiteSpace);

   /// <summary>Determines whether the given string is not null or consists of only white spaces.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string is not null or consists of only white spaces; otherwise, false.</returns>
   public static bool IsNotNullOrWhiteSpace(this string? value)
      => !value.IsNullOrWhiteSpace();

   /// <summary>Determines whether the given string is not null or whitespace.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string is not null or whitespace; otherwise, false.</returns>
   public static bool IsExist(this string? value)
      => !value.IsNullOrWhiteSpace();

   /// <summary>Determines whether the given string contains Unicode characters.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>True if the string contains Unicode characters; otherwise, false.</returns>
   public static bool IsUnicode(this string value)
   {
      ArgumentNullException.ThrowIfNull(value);
      // Optimized: check if all characters are ASCII first
      return value.Any(c => c > 127);
   }

   /// <summary>Ensures that the given string starts with a leading slash.</summary>
   /// <param name="value">The string to ensure leading slash for.</param>
   /// <returns>The string with a leading slash, or the original string if it already starts with a slash.</returns>
   public static string? EnsureLeadingSlash(this string? value)
      => value.IsNotNullOrWhiteSpace() && !value.StartsWith('/')
         ? "/" + value
         : value;

   /// <summary>Ensures that the given string has a trailing slash.</summary>
   /// <param name="value">The string to check.</param>
   /// <returns>The string with a trailing slash if it doesn't have one already; otherwise, the original string.</returns>
   public static string? EnsureTrailingSlash(this string? value)
      => value.IsNotNullOrWhiteSpace() && !value!.EndsWith('/')
         ? value + "/"
         : value;

   /// <summary>Removes the leading slash from the given string.</summary>
   /// <param name="value">The string to remove the leading slash from.</param>
   /// <returns>The string without the leading slash.</returns>
   public static string? RemoveLeadingSlash(this string? value)
      => value.IsNotNullOrWhiteSpace() && value!.StartsWith('/')
         ? value[1..] // Use span-based slice instead of Substring
         : value;

   /// <summary>Removes the trailing slash from the given string.</summary>
   /// <param name="value">The string to remove the trailing slash from.</param>
   /// <returns>The string without the trailing slash.</returns>
   public static string? RemoveTrailingSlash(this string? value)
      => value.IsNotNullOrWhiteSpace() && value!.EndsWith('/')
         ? value[..^1] // Use span-based slice instead of Substring
         : value;

   /// <summary>Ensures that the given string starts with the specified character.</summary>
   /// <param name="value">The string to ensure the start with.</param>
   /// <param name="c">The character to check.</param>
   /// <returns>The string with the specified character at the beginning, if it is not already there; otherwise, the original string.</returns>
   public static string EnsureStartsWith([NotNull] this string value, char c)
      => value.EnsureStartsWith(c, StringComparison.Ordinal);

   /// <summary>Ensures that the given string starts with the specified character.</summary>
   /// <param name="value">The string to ensure the start with character.</param>
   /// <param name="c">The character to ensure at the start of the string.</param>
   /// <param name="comparison">The type of string comparison to use.</param>
   /// <returns>The original string if it already starts with the specified character, otherwise a new string with the specified character added at the start.</returns>
   public static string EnsureStartsWith([NotNull] this string value, char c, StringComparison comparison)
      => value.ThrowIfNull().StartsWith(c.ToString(), comparison)
         ? value
         : c + value;

   /// <summary>Ensures that the given string starts with the specified character.</summary>
   /// <param name="value">The string to check.</param>
   /// <param name="c">The character to ensure the string starts with.</param>
   /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
   /// <param name="culture">The culture to use for the string comparison.</param>
   /// <returns>The original string if it already starts with the character; otherwise, a new string with the character prepended.</returns>
   public static string EnsureStartsWith([NotNull] this string value, char c, bool ignoreCase, CultureInfo culture)
      => value.ThrowIfNull().StartsWith(c.ToString(culture), ignoreCase, culture)
         ? value
         : c + value;

   /// <summary>Ensures that the given string ends with the specified character.</summary>
   /// <param name="value">The string to check and modify.</param>
   /// <param name="c">The character to ensure at the end of the string.</param>
   /// <returns>The modified string with the character ensured at the end. If the string already ends with the character, the original string is returned.</returns>
   public static string EnsureEndsWith([NotNull] this string value, char c)
      => value.EnsureEndsWith(c, StringComparison.Ordinal);

   /// <summary>Ensures that the given string ends with the specified character.</summary>
   /// <param name="value">The string to ensure the end with.</param>
   /// <param name="c">The character to ensure the string ends with.</param>
   /// <param name="comparison">The type of string comparison to use.</param>
   /// <returns>The original string if it already ends with the specified character; otherwise, a new string with the specified character appended at the end.</returns>
   public static string EnsureEndsWith([NotNull] this string value, char c, StringComparison comparison)
      => value.ThrowIfNull().EndsWith(c.ToString(), comparison)
         ? value
         : value + c;

   /// <summary>Ensures that the given string ends with the specified character. If the string already ends with the character, it returns the original string. Otherwise, it appends the character to the end of the string.</summary>
   /// <param name="value">The string to ensure the ending character.</param>
   /// <param name="c">The character to be ensured at the end of the string.</param>
   /// <param name="ignoreCase">True to ignore case; otherwise, false.</param>
   /// <param name="culture">The culture to use for the string comparison.</param>
   /// <returns>The original string if it already ends with the specified character; otherwise, a new string with the specified character appended at the end.</returns>
   public static string EnsureEndsWith([NotNull] this string value, char c, bool ignoreCase, CultureInfo culture)
      => value.ThrowIfNull().EndsWith(c.ToString(culture), ignoreCase, culture)
         ? value
         : value + c;

   /// <summary>Matches a regular expression pattern against the specified input string.</summary>
   /// <param name="regex">The regular expression pattern to match.</param>
   /// <param name="source">The input string to match against the regular expression pattern.</param>
   /// <returns>The first occurrence of a regular expression matches in the input string if it exists; otherwise, an empty <see cref="Match"/>.</returns>
   public static Match RegexMatch(this Regex regex, string source)
   {
      regex.ThrowIfNull();
      source.ThrowIfNull();

      var match = regex.Match(source);
      return match.Success ? match : Match.Empty;
   }

   /// <summary>Retrieves a specified number of characters from the left side of a string.</summary>
   /// <param name="value">The string from which to retrieve the characters.</param>
   /// <param name="length">The number of characters to retrieve.</param>
   /// <returns>A string that contains the specified number of characters from the left side of the input string.</returns>
   public static string Left([NotNull] this string value, int length)
   {
      value.ThrowIfNull();
      value.Length.ThrowIfLessThan(length, nameof(length));

      return value[..length];
   }

   /// <summary>Retrieves a substring from the given string starting from the rightmost position.</summary>
   /// <param name="value">The string to extract the substring from.</param>
   /// <param name="length">The length of the substring to retrieve.</param>
   /// <returns>The substring extracted from the rightmost position.</returns>
   /// <exception cref="ArgumentNullException">Thrown when the <paramref name="value"/> is null.</exception>
   /// <exception cref="ArgumentLessThanException">Thrown when the length is greater than the length of the string.</exception>
   public static string Right(this string value, int length)
   {
      value.ThrowIfNull();
      value.Length.ThrowIfLessThan(length);

      return value[^length..]; // Use span-based slice from end
   }

   /// <summary>Removes all occurrences of specified strings from the source string.</summary>
   /// <param name="source">The source string.</param>
   /// <param name="strings">The strings to be removed.</param>
   /// <returns>A new string with all occurrences of the specified strings removed.</returns>
   public static string RemoveAll([NotNull] this string source, params string[] strings)
      => strings.ThrowIfNull().Aggregate(source.ThrowIfNull(), ReplaceOrdinal);

   /// <summary>Replaces all occurrences of a target string with an empty string in the specified string using ordinal comparison.</summary>
   /// <param name="current">The string to perform the replacement on.</param>
   /// <param name="target">The target string to be replaced.</param>
   /// <returns>The string after the replacement.</returns>
   public static string ReplaceOrdinal([NotNull] this string current, string target)
      => current.ThrowIfNull().Replace(target.ThrowIfNull(), "", StringComparison.Ordinal);

   /// <summary>Removes the specified prefixes from the given string.</summary>
   /// <param name="value">The string from which to remove the prefixes.</param>
   /// <param name="prefixes">The prefixes to remove.</param>
   /// <returns>The string with the specified prefixes removed.</returns>
   public static string RemovePreFixes([NotNull] this string value, params string[] prefixes)
      => value.ThrowIfNull().RemovePreFixes(true, CultureInfo.InvariantCulture, prefixes);

   /// <summary>Removes the specified prefixes from the given string based on the provided comparison type.</summary>
   /// <param name="value">The string from which to remove the prefixes.</param>
   /// <param name="comparison">The type of string comparison to use.</param>
   /// <param name="prefixes">The prefixes to remove.</param>
   /// <returns>The resulting string after removing the prefixes.</returns>
   public static string RemovePreFixes([NotNull] this string value, StringComparison comparison, params string[] prefixes)
   {
      value.ThrowIfNull();

      if (value.IsNullOrEmpty())
      {
         return string.Empty;
      }

      if (prefixes.IsNullOrEmpty())
      {
         return value;
      }

      foreach (var prefix in prefixes)
         if (value.StartsWith(prefix, comparison))
         {
            return value.Right(value.Length - prefix.Length);
         }

      return value;
   }

   /// <summary>Removes the specified prefixes from the given string, optionally ignoring case and considering culture.</summary>
   /// <param name="value">The string from which to remove the prefixes.</param>
   /// <param name="ignoreCase">A boolean value indicating whether to ignore case when comparing the prefixes. Default is true.</param>
   /// <param name="culture">The culture to use when comparing strings. Default is null.</param>
   /// <param name="prefixes">The prefixes to remove.</param>
   /// <returns>The modified string after removing the prefixes.</returns>
   public static string RemovePreFixes([NotNull] this string value, bool ignoreCase, CultureInfo culture, params string[] prefixes)
   {
      value.ThrowIfNull();

      if (value.IsNullOrEmpty())
      {
         return string.Empty;
      }

      if (prefixes.IsNullOrEmpty())
      {
         return value;
      }

      foreach (var prefix in prefixes)
         if (value.StartsWith(prefix, ignoreCase, culture))
         {
            return value.Right(value.Length - prefix.Length);
         }

      return value;
   }

   /// <summary>Removes specified postfixes from the given string.</summary>
   /// <param name="value">The string to remove postfixes from.</param>
   /// <param name="postfixes">The postfixes to remove.</param>
   /// <returns>The string with postfixes removed.</returns>
   public static string RemovePostFixes([NotNull] this string value, params string[] postfixes)
      => value.RemovePostFixes(true, CultureInfo.InvariantCulture, postfixes);

   /// <summary>Removes the specified postfixes from the given string using the specified string comparison.</summary>
   /// <param name="value">The string from which to remove the postfixes.</param>
   /// <param name="comparison">The type of string comparison to use. </param>
   /// <param name="postfixes">The postfixes to remove.</param>
   /// <returns>The modified string with the postfixes removed.</returns>
   public static string RemovePostFixes([NotNull] this string value, StringComparison comparison, params string[] postfixes)
   {
      value.ThrowIfNull();

      if (value.IsNullOrEmpty())
      {
         return string.Empty;
      }

      if (postfixes.IsNullOrEmpty())
      {
         return value;
      }

      foreach (var postfix in postfixes)
         if (value.EndsWith(postfix, comparison))
         {
            return value.Left(value.Length - postfix.Length);
         }

      return value;
   }

   /// <summary>Removes specified postfixes from the given string.</summary>
   /// <param name="value">The string to remove postfixes from.</param>
   /// <param name="ignoreCase">A boolean value indicating whether to ignore the case when comparing postfixes.</param>
   /// <param name="culture">The culture used for string comparison.</param>
   /// <param name="postfixes">The postfixes to remove.</param>
   /// <returns>The modified string with the postfixes removed.</returns>
   public static string RemovePostFixes([NotNull] this string value, bool ignoreCase, CultureInfo culture, params string[] postfixes)
   {
      value.ThrowIfNull();

      if (value.IsEmpty())
      {
         return string.Empty;
      }

      if (postfixes.IsNullOrEmpty())
      {
         return value;
      }

      foreach (var postfix in postfixes)
         if (value.EndsWith(postfix, ignoreCase, culture))
         {
            return value.Left(value.Length - postfix.Length);
         }

      return value;
   }

   /// <summary>Splits the given string into substrings of the specified size.</summary>
   /// <param name="value">The string to split.</param>
   /// <param name="size">The size of each substring.</param>
   /// <returns>An enumerable collection of substrings.</returns>
   public static IEnumerable<string> Split([NotNull] this string value, int size)
   {
      value.ThrowIfNull().ThrowIfEmpty().ThrowIfNullOrWhitespace<ArgumentEmptyException>();
      return Enumerable.Range(0, value.Length / size).Select(index => value.Substring(index * size, size));
   }

   /// <summary>Converts the specified string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.</summary>
   /// <typeparam name="T">The enumeration type.</typeparam>
   /// <param name="value">A string containing the name or value to convert.</param>
   /// <param name="ignoreCase">A Boolean value that indicates whether to ignore case during the conversion. The default is true.</param>
   /// <returns>An object of type T whose value is represented by value.</returns>
   public static T ToEnum<T>([NotNull] this string value, bool ignoreCase = true)
      where T : Enum
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();
      value.ThrowIfNullOrWhitespace();

      return EnumValues<T>.Parse(value, ignoreCase);
   }

   /// <summary>Truncates the given string to a specified maximum length.</summary>
   /// <param name="value">The string to truncate.</param>
   /// <param name="maxLength">The maximum length of the truncated string.</param>
   /// <returns>The truncated string.</returns>
   public static string Truncate([NotNull] this string value, int maxLength)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();
      maxLength.ThrowIfLessThan(0);
      value.Length.ThrowIfLessThan(maxLength);

      return value.Left(maxLength);
   }

   /// <summary>Truncates the given string if it exceeds the maximum length, appending a postfix.</summary>
   /// <param name="value">The string to truncate.</param>
   /// <param name="maxLength">The maximum length of the truncated string.</param>
   /// <param name="postfix">The postfix to append to the truncated string. Default is "..."</param>
   /// <returns>The truncated string with the postfix.</returns>
   public static string TruncateWithPostfix([NotNull] this string value, int maxLength, string postfix = "...")
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();
      maxLength.ThrowIfLessThan(0);
      maxLength.ThrowIfLessThan(postfix.Length);

      return value.Length <= maxLength
         ? value
         : value.Left(maxLength - postfix.Length) + postfix;
   }

   /// <summary>Safely retrieves a substring from the given string starting from the specified index.</summary>
   /// <param name="value">The string from which to retrieve the substring.</param>
   /// <param name="start">The starting index of the substring. Defaults to 0.</param>
   /// <returns>The substring starting from the specified index.</returns>
   public static string SubstringSafe([NotNull] this string value, int start = 0)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();
      start.ThrowIfLessThan(-1);

      return value.SubstringSafe(start, value.Length);
   }

   /// Safely returns a substring of the given string, starting from the specified index and with the specified length.
   /// If the length exceeds the remaining characters from the start index, the method will return the remaining characters.
   /// </summary>
   /// <param name="value">The string to extract the substring from.</param>
   /// <param name="start">The starting index of the substring.</param>
   /// <param name="length">The length of the substring.</param>
   /// <returns>The substring of the given string starting from the specified index and with the specified length.</returns>
   public static string SubstringSafe(this string value, int start, int length)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();
      start.ThrowIfLessThan(-1);
      length.ThrowIfLessThan(0);

      return value.Length - start > length
         ? value.Substring(start, length)
         : value[start..];
   }

   /// <summary>Converts the given string to title case.</summary>
   /// <param name="value">The string to convert.</param>
   /// <returns>The converted string in title case.</returns>
   public static string ToTitleCase([NotNull] this string value)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();
      value = value.ToLower();

      return string.Concat(value.AsSpan(0, 1).ToString().ToUpper(), value.AsSpan(1));
   }

   /// <summary>Escapes special characters in a search string.</summary>
   /// <param name="value">The search string to escape.</param>
   /// <returns>The escaped search string.</returns>
   public static string EscapeSearch(this string value)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();

      // Pre-allocate with estimated capacity to reduce resizing
      var result = new StringBuilder(value.Length * 2);

      foreach (var ch in value)
      {
         if (SpecialCharacters.Contains(ch))
         {
            result.Append('\\');
         }

         result.Append(ch);
      }

      result.Replace("&&", @"\&&");
      result.Replace("||", @"\||");

      return result.ToString().Trim();
   }

   /// <summary>Escapes special characters in a string to be used as a selector in regular expressions.</summary>
   /// <param name="value">The string to escape.</param>
   /// <returns>The escaped string.</returns>
   public static string EscapeSelector([NotNull] this string value)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();

      var pattern     = string.Format("([{0}])", "/[!\"#$%&'()*+,./:;<=>?@^`{{|}}~\\]");
      var replacement = @"\\$1";
      return Regex.Replace(value, pattern, replacement, RegexOptions.Compiled, Constants.RegexTimeout);
   }

   /// <summary>Removes accents from the given string.</summary>
   /// <param name="value">The string to remove accents from.</param>
   /// <returns>The string without accents.</returns>
   public static string RemoveAccent([NotNull] this string value)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();

      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      var encoding = Encoding.GetEncoding("Cyrillic");
      var bytes    = encoding.GetBytes(value);
      return Encoding.ASCII.GetString(bytes);
   }

   /// <summary>Generates a slug from the given string value.</summary>
   /// <param name="value">The string value to generate the slug from.</param>
   /// <returns>The generated slug.</returns>
   public static string GenerateSlug(this string value)
   {
      value.ThrowIfNull();
      value.ThrowIfEmpty();

      var str = value.RemoveAccent().ToLower();

      // invalid chars
      str = InvalidCharsRegex.Replace(str, "");
      // convert multiple spaces into one space
      str = MultipleSpacesRegex.Replace(str, " ").Trim();
      // cut and trim it - use span-based slice
      str = str.Length <= 240 ? str : str[..240].Trim();
      // hyphens
      str = SpaceToHyphenRegex.Replace(str, "-");

      return str;
   }

   /// <summary>Determines whether two strings are equal ignoring case.</summary>
   /// <param name="str1">The first string to compare.</param>
   /// <param name="str2">The second string to compare.</param>
   /// <returns>True if the strings are equal ignoring case; otherwise, false.</returns>
   public static bool EqualsInvariant([NotNull] this string str1, string str2)
      => string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);

   /// <summary>Joins a collection of strings into a single string, with each element separated by a space character.</summary>
   /// <param name="list">The collection of strings to join.</param>
   /// <returns>The joined string with elements separated by space.</returns>
   /// <exception cref="ArgumentNullException">Thrown when the input collection is null.</exception>
   /// <exception cref="ArgumentEmptyException">Thrown when the input collection is empty.</exception>
   public static string SeparateToSpace([NotNull] this IEnumerable<string> list)
   {
      list.ThrowIfNull();
      list.ThrowIfEmpty();
      return !list.Any()
         ? throw new ArgumentEmptyException(nameof(list))
         : string.Join(' ', list);
   }

   /// <summary>Separates the given string into individual substrings based on space delimiter and returns them as a list.</summary>
   /// <param name="value">The string to be separated.</param>
   /// <returns>A list of individual substrings after separating the input string based on space delimiter.</returns>
   public static IEnumerable<string> SeparateFromSpace([NotNull] this string value)
      => value.ThrowIfNull()
              .ThrowIfEmpty()
              .Trim()
              .Split(Separator, StringSplitOptions.RemoveEmptyEntries);
}