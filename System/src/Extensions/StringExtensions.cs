// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions;

[DebuggerStepThrough]
public static class StringExtensions
{
	public static bool IsEmpty(this string value)
		=> value == string.Empty;

	public static bool IsNullOrEmpty(this string value)
		=> string.IsNullOrEmpty(value);

	public static bool IsNotNullOrEmpty(this string value)
		=> !string.IsNullOrEmpty(value);

	public static bool IsNullOrWhiteSpace(this string value)
		=> string.IsNullOrWhiteSpace(value);

	public static bool IsNotNullOrWhiteSpace(this string value)
		=> !value.IsNullOrWhiteSpace();

	public static bool IsExist(this string value)
		=> !value.IsNullOrWhiteSpace();

	public static bool IsUnicode(this string value)
		=> Encoding.ASCII.GetByteCount(value) != Encoding.UTF8.GetByteCount(value);

	public static string EnsureLeadingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && !value.StartsWith("/")
			   ? "/" + value
			   : value;

	public static string EnsureTrailingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && !value.EndsWith("/")
			   ? value + "/"
			   : value;

	public static string RemoveLeadingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && value.StartsWith("/")
			   ? value.Substring(1)
			   : value;

	public static string RemoveTrailingSlash(this string value)
		=> value.IsNotNullOrWhiteSpace() && value.EndsWith("/")
			   ? value.Substring(0, value.Length - 1)
			   : value;

	public static string EnsureStartsWith(this string value, char c)
		=> value.EnsureStartsWith(c, StringComparison.Ordinal);

	public static string EnsureStartsWith(this string value, char c, StringComparison comparison)
		=> value.ThrowIfNull().StartsWith(c.ToString(), comparison)
			   ? value
			   : c + value;

	public static string EnsureStartsWith(this string value, char c, bool ignoreCase, CultureInfo culture)
		=> value.ThrowIfNull().StartsWith(c.ToString(culture), ignoreCase, culture)
			   ? value
			   : c + value;

	public static string EnsureEndsWith(this string value, char c)
		=> value.EnsureEndsWith(c, StringComparison.Ordinal);

	public static string EnsureEndsWith(this string value, char c, StringComparison comparison)
		=> value.ThrowIfNull().EndsWith(c.ToString(), comparison)
			   ? value
			   : value + c;

	public static string EnsureEndsWith(this string value, char c, bool ignoreCase, CultureInfo culture)
		=> value.ThrowIfNull().EndsWith(c.ToString(culture), ignoreCase, culture)
			   ? value
			   : value + c;

	public static Match RegexMatch(this Regex regex, string source)
	{
		regex.ThrowIfNull();
		source.ThrowIfNull();

		var match = regex.Match(source);
		return match.Success ? match : Match.Empty;
	}

	public static string Left(this string value, int length)
	{
		value.ThrowIfNull();
		value.Length.ThrowIfLessThan(length, nameof(length));

		return value.Substring(0, length);
	}

	public static string Right(this string value, int length)
	{
		value.ThrowIfNull();
		value.Length.ThrowIfLessThan(length);

		return value.Substring(value.Length - length, length);
	}

	public static string RemoveAll(this string source, params string[] strings)
		=> strings.ThrowIfNull().Aggregate(source.ThrowIfNull(), ReplaceOrdinal);

	private static string ReplaceOrdinal(string current, string target)
		=> current.ThrowIfNull().Replace(target.ThrowIfNull(), "", StringComparison.Ordinal);

	public static string RemovePreFixes(this string value, params string[] prefixes)
		=> value.ThrowIfNull().RemovePreFixes(StringComparison.OrdinalIgnoreCase, prefixes);

	public static string RemovePreFixes(this string value, StringComparison comparison, params string[] prefixes)
		=> value.ThrowIfNull().RemovePreFixes(true, CultureInfo.InvariantCulture, prefixes);

	public static string RemovePreFixes(this string value, bool ignoreCase, CultureInfo culture, params string[] prefixes)
	{
		value.ThrowIfNull();

		if (value.IsNullOrEmpty())
			return string.Empty;
		if (prefixes.IsNullOrEmpty())
			return value;

		foreach (var prefix in prefixes)
			if (value.StartsWith(prefix, ignoreCase, culture))
				return value.Right(value.Length - prefix.Length);

		return value;
	}

	public static string RemovePostFixes(this string value, params string[] postfixes)
		=> value.RemovePostFixes(StringComparison.OrdinalIgnoreCase, postfixes);

	public static string RemovePostFixes(this string value, StringComparison comparison, params string[] postfixes)
		=> value.RemovePostFixes(true, CultureInfo.InvariantCulture, postfixes);

	public static string RemovePostFixes(this string value, bool ignoreCase, CultureInfo culture, params string[] postfixes)
	{
		value.ThrowIfNull();

		if (value.IsNullOrEmpty())
			return string.Empty;
		if (postfixes.IsNullOrEmpty())
			return value;

		foreach (var postfix in postfixes)
			if (value.EndsWith(postfix, ignoreCase, culture))
				return value.Left(value.Length - postfix.Length);

		return value;
	}

	public static IEnumerable<string> Split(this string value, int size)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();
		
		var chuck = value.Length/size;
		return Enumerable.Range(0, chuck )
		                 .Select(index => value.Substring(index * size, size));
	}

	public static T ToEnum<T>(this string value, bool ignoreCase = true)
		where T : struct
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();
		value.ThrowIfNullOrWhitespace();

		return (T)Enum.Parse(typeof(T), value, ignoreCase);
	}

	public static string Truncate(this string value, int maxLength)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();
		maxLength.ThrowIfLessThan(0);
		value.Length.ThrowIfLessThan(maxLength);

		return value.Left(maxLength);
	}

	public static string TruncateWithPostfix(this string value, int maxLength, string postfix = "...")
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();
		maxLength.ThrowIfLessThan(0);
		maxLength.ThrowIfLessThan(postfix.Length);

		return value.Length <= maxLength
			       ? value
			       : value.Left(maxLength - postfix.Length) + postfix;
	}

	public static string SubstringSafe(this string value, int start = 0)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();
		start.ThrowIfLessThan(-1);

		return value.SubstringSafe(start, value.Length);
	}

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

	public static string ToTitleCase(this string value)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();
		value = value.ToLower();

		return value.First().ToString().ToUpper() + value.Substring(1);
	}

	public static string EscapeSearch(this string value)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();

		char[] specialCharacters = { '+', '-', '!', '(', ')', '{', '}', '[', ']', '^', '"', '~', '*', '?', ':', '\\' };
		var    result            = new StringBuilder("");
		//'&&', '||',
		foreach (var ch in value)
		{
			if (specialCharacters.Any(x => x == ch))
				result.Append("\\");

			result.Append(ch);
		}

		result = result.Replace("&&", @"\&&");
		result = result.Replace("||", @"\||");

		return result.ToString().Trim();
	}

	public static string EscapeSelector(this string value)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();

		var pattern     = string.Format("([{0}])", "/[!\"#$%&'()*+,./:;<=>?@^`{{|}}~\\]");
		var replacement = @"\\$1";
		return Regex.Replace(value, pattern, replacement, RegexOptions.Compiled, Constants.RegexTimeout);
	}

	public static string RemoveAccent(this string value)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();

		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		var    encoding = Encoding.GetEncoding("Cyrillic");
		byte[] bytes    = encoding.GetBytes(value);
		return Encoding.ASCII.GetString(bytes);
	}

	public static string GenerateSlug(this string value)
	{
		value.ThrowIfNull();
		value.ThrowIfEmpty();

		string str = value.RemoveAccent().ToLower();

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

	public static bool EqualsInvariant(this string str1, string str2)
		=> string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);

	public static string SeparateToSpace(this IEnumerable<string> list)
	{
		list.ThrowIfNull();
		list.ThrowIfEmpty();
		return !list.Any()
			       ? throw new ArgumentEmptyException(nameof(list))
			       : string.Join(' ', list);
	}

	public static IEnumerable<string> SeparateFromSpace(this string value)
		=> value.ThrowIfNull()
		        .ThrowIfEmpty()
		        .Trim()
		        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
}