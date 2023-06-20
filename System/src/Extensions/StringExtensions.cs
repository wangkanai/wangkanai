// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Wangkanai.Extensions;

public static class StringExtensions
{
	[DebuggerStepThrough]
	public static bool IsNullOrEmpty(this string value)
		=> string.IsNullOrEmpty(value);

	[DebuggerStepThrough]
	public static bool IsNotNullOrEmpty(this string value)
		=> !string.IsNullOrEmpty(value);

	[DebuggerStepThrough]
	public static bool IsNullOrWhiteSpace(this string value)
		=> string.IsNullOrWhiteSpace(value);

	[DebuggerStepThrough]
	public static bool IsNotNullOrWhiteSpace(this string value)
		=> !value.IsNullOrWhiteSpace();

	[DebuggerStepThrough]
	public static bool IsExist(this string value)
		=> !value.IsNullOrWhiteSpace();

	[DebuggerStepThrough]
	public static bool IsUnicode(this string value)
		=> Encoding.ASCII.GetByteCount(value) != Encoding.UTF8.GetByteCount(value);

	[DebuggerStepThrough]
	public static string EnsureLeadingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && !value.StartsWith("/")
			   ? "/" + value
			   : value;

	[DebuggerStepThrough]
	public static string EnsureTrailingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && !value.EndsWith("/")
			   ? value + "/"
			   : value;

	[DebuggerStepThrough]
	public static string RemoveLeadingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && value.StartsWith("/")
			   ? value.Substring(1)
			   : value;

	[DebuggerStepThrough]
	public static string RemoveTrailingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && value.EndsWith("/")
			   ? value.Substring(0, value.Length - 1)
			   : value;

	[DebuggerStepThrough]
	public static string EnsureStartsWith(this string value, char c)
		=> value.EnsureStartsWith(c, StringComparison.Ordinal);

	[DebuggerStepThrough]
	public static string EnsureStartsWith(this string value, char c, StringComparison comparison)
		=> value.ThrowIfNull().StartsWith(c.ToString(), comparison)
			   ? value
			   : c + value;

	[DebuggerStepThrough]
	public static string EnsureStartsWith(this string value, char c, bool ignoreCase, CultureInfo culture)
		=> value.ThrowIfNull().StartsWith(c.ToString(culture), ignoreCase, culture)
			   ? value
			   : c + value;

	[DebuggerStepThrough]
	public static string EnsureEndsWith(this string value, char c)
		=> value.EnsureEndsWith(c, StringComparison.Ordinal);

	[DebuggerStepThrough]
	public static string EnsureEndsWith(this string value, char c, StringComparison comparison)
		=> value.ThrowIfNull().EndsWith(c.ToString(), comparison)
			   ? value
			   : value + c;

	[DebuggerStepThrough]
	public static string EnsureEndsWith(this string value, char c, bool ignoreCase, CultureInfo culture)
		=> value.ThrowIfNull().EndsWith(c.ToString(culture), ignoreCase, culture)
			   ? value
			   : value + c;

	[DebuggerStepThrough]
	public static Match RegexMatch(this Regex regex, string source)
	{
		var match = regex.Match(source);
		return match.Success ? match : Match.Empty;
	}

	[DebuggerStepThrough]
	public static string Left(this string value, int length)
	{
		value.ThrowIfNull();
		value.Length.ThrowIfLessThan(length, nameof(length));

		return value.Substring(0, length);
	}

	[DebuggerStepThrough]
	public static string Right(this string value, int length)
	{
		value.ThrowIfNull();
		value.Length.ThrowIfLessThan(length);

		return value.Substring(value.Length - length, length);
	}

	[DebuggerStepThrough]
	public static string RemoveAll(this string source, params string[] strings)
		=> strings.Aggregate(source, ReplaceOrdinal);

	[DebuggerStepThrough]
	private static string ReplaceOrdinal(string current, string target)
		=> current.Replace(target, "", StringComparison.Ordinal);

	[DebuggerStepThrough]
	public static string RemovePreFixes(this string value, params string[] prefixes)
		=> value.RemovePreFixes(StringComparison.OrdinalIgnoreCase, prefixes);

	[DebuggerStepThrough]
	public static string RemovePreFixes(this string value, StringComparison comparison, params string[] prefixes)
		=> value.RemovePreFixes(true, CultureInfo.InvariantCulture, prefixes);

	[DebuggerStepThrough]
	public static string RemovePreFixes(this string value, bool ignoreCase, CultureInfo culture, params string[] prefixes)
	{
		if (value.TrueIfNull())
			return null;
		if (value.IsNullOrEmpty())
			return string.Empty;
		if (prefixes.IsNullOrEmpty())
			return value;

		foreach (var prefix in prefixes)
			if (value.StartsWith(prefix, ignoreCase, culture))
				return value.Right(value.Length - prefix.Length);

		return value;
	}

	[DebuggerStepThrough]
	public static string RemovePostFixes(this string value, params string[] postfixes)
		=> value.RemovePostFixes(StringComparison.OrdinalIgnoreCase, postfixes);

	[DebuggerStepThrough]
	public static string RemovePostFixes(this string value, StringComparison comparison, params string[] postfixes)
		=> value.RemovePostFixes(true, CultureInfo.InvariantCulture, postfixes);

	[DebuggerStepThrough]
	public static string RemovePostFixes(this string value, bool ignoreCase, CultureInfo culture, params string[] postfixes)
	{
		if (value is null)
			return null;
		if (value.IsNullOrEmpty())
			return string.Empty;
		if (postfixes.IsNullOrEmpty())
			return value;

		foreach (var postfix in postfixes)
			if (value.EndsWith(postfix, ignoreCase, culture))
				return value.Left(value.Length - postfix.Length);

		return value;
	}

	[DebuggerStepThrough]
	public static T ToEnum<T>(this string value, bool ignoreCase = true) 
		where T : struct
	{
		value.ThrowIfNull();

		return (T)Enum.Parse(typeof(T), value, ignoreCase);
	}

	[DebuggerStepThrough]
	public static string Truncate(this string value, int maxLength)
	{
		value.ThrowIfNull();
		maxLength.ThrowIfLessThan(0);

		return value.Length <= maxLength
			       ? value
			       : value.Left(maxLength);
	}

	[DebuggerStepThrough]
	public static string TruncateWithPostfix(this string value, int maxLength = Int32.MaxValue, string postfix = "...")
	{
		if (value.TrueIfNull())
			return null;
		if (value.IsNullOrEmpty() || maxLength == 0)
			return string.Empty;
		if (value.Length <= maxLength)
			return value;
		if (maxLength <= postfix.Length)
			return postfix.Left(maxLength);

		return value.Left(maxLength - postfix.Length) + postfix;
	}

	[DebuggerStepThrough]
	public static string SubstringSafe(this string value, int start = 0, int length = 0)
	{
		if (value.Length <= start)
			return "";
		return value.Length - start <= length
			       ? value[start..]
			       : value.Substring(start, length);
	}

	[DebuggerStepThrough]
	public static string SubstringSafe(this string value, int start = 0)
		=> value.Length <= start
			   ? ""
			   : value[start..];

	[DebuggerStepThrough]
	public static string ToTitleCase(this string value)
		=> value.First().ToString().ToUpper() + value.Substring(1);

	[DebuggerStepThrough]
	public static string EscapeSearchTerm(this string term)
	{
		char[] specialCharacters = { '+', '-', '!', '(', ')', '{', '}', '[', ']', '^', '"', '~', '*', '?', ':', '\\' };
		var    result            = new StringBuilder("");
		//'&&', '||',
		foreach (var ch in term)
		{
			if (specialCharacters.Any(x => x == ch))
				result.Append("\\");

			result.Append(ch);
		}

		result = result.Replace("&&", @"\&&");
		result = result.Replace("||", @"\||");

		return result.ToString().Trim();
	}

	public static string EscapeSelector(this string attribute)
		=> Regex.Replace(attribute, string.Format("([{0}])", "/[!\"#$%&'()*+,./:;<=>?@^`{|}~\\]"), @"\\$1", RegexOptions.Compiled, Constants.RegexTimeout);

	public static string GenerateSlug(this string phrase)
	{
		string str = phrase.RemoveAccent().ToLower();

		// invalid chars
		str = Regex.Replace(str, @"[^a-z0-9\s-]", "", RegexOptions.Compiled, Constants.RegexTimeout);
		// convert multiple spaces into one space
		str = Regex.Replace(str, @"\s+", " ", RegexOptions.Compiled, Constants.RegexTimeout).Trim();
		// cut and trim it
		str = str.Substring(0, str.Length <= 240 ? str.Length : 240).Trim();
		// hyphens
		str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled, Constants.RegexTimeout);

		return str;
	}

	[DebuggerStepThrough]
	public static string RemoveAccent(this string value)
	{
		byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
		return Encoding.ASCII.GetString(bytes);
	}

	[DebuggerStepThrough]
	public static bool EqualsInvariant(this string str1, string str2)
		=> string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);

	[DebuggerStepThrough]
	public static string ToSpaceSeparatedString(this IEnumerable<string> list)
		=> list.IsNullOrEmpty()
			   ? string.Empty
			   : string.Join(' ', list);

	[DebuggerStepThrough]
	public static IEnumerable<string> FromSpaceSeparatedString(this string value)
		=> value.Trim()
		        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
}