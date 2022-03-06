// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Wangkanai.Extensions;

public static class StringExtensions
{
    [ContractAnnotation("null => true")]
    public static bool IsNullOrEmpty(this string input)
        => string.IsNullOrEmpty(input);

    [ContractAnnotation("null => true")]
    public static bool IsNullOrWhiteSpace(this string input)
        => string.IsNullOrWhiteSpace(input);

    public static bool IsUnicode(this string input)
        => Encoding.ASCII.GetByteCount(input) != Encoding.UTF8.GetByteCount(input);

    public static string EnsureEndsWith(this string input, char c)
        => input.EnsureEndsWith(c, StringComparison.Ordinal);

    public static string EnsureEndsWith(this string input, char c, StringComparison comparison)
    {
        Check.NotNull(input);

        return input.EndsWith(c.ToString(), comparison)
                   ? input
                   : input + c;
    }

    public static string EnsureEndsWith(this string input, char c, bool ignoreCase, CultureInfo culture)
    {
        Check.NotNull(input);

        return input.EndsWith(c.ToString(culture), ignoreCase, culture)
                   ? input
                   : input + c;
    }

    public static string EnsureStartsWith(this string input, char c)
        => input.EnsureStartsWith(c, StringComparison.Ordinal);

    public static string EnsureStartsWith(this string input, char c, StringComparison comparison)
    {
        Check.NotNull(input);

        return input.StartsWith(c.ToString(), comparison)
                   ? input
                   : c + input;
    }

    public static string EnsureStartsWith(this string input, char c, bool ignoreCase, CultureInfo culture)
    {
        Check.NotNull(input);

        return input.StartsWith(c.ToString(culture), ignoreCase, culture)
                   ? input
                   : c + input;
    }

    public static Match RegexMatch(this Regex regex, string source)
    {
        var match = regex.Match(source);
        return match.Success ? match : Match.Empty;
    }


    public static string Left(this string input, int length)
    {
        Check.NotNull(input);
        Check.NotLessThan(input.Length, length, nameof(length));

        return input.Substring(0, length);
    }

    public static string Right(this string input, int length)
    {
        Check.NotNull(input);
        Check.NotMoreThan(input.Length, length);

        return input.Substring(input.Length - length, length);
    }

    public static string RemoveAll(this string source, params string[] strings)
        => strings.Aggregate(source, ReplaceOrdinal);

    private static string ReplaceOrdinal(string current, string target)
        => current.Replace(target, "", StringComparison.Ordinal);

    public static string RemovePostFix(this string value, params string[] postfixes)
    {
        if (value is null)
            return null;
        if (value.IsNullOrEmpty())
            return string.Empty;
        if (postfixes.IsNullOrEmpty())
            return value;

        foreach (var postfix in postfixes)
            if (value.EndsWith(postfix))
                return value.Left(value.Length - postfix.Length);

        return value;
    }

    public static string RemovePreFix(this string value, params string[] prefixes)
    {
        if (value is null)
            return null;
        if (value.IsNullOrEmpty())
            return string.Empty;
        if (prefixes.IsNullOrEmpty())
            return value;

        foreach (var prefix in prefixes)
            if (value.StartsWith(prefix))
                return value.Right(value.Length - prefix.Length);

        return value;
    }

    public static T ToEnum<T>(this string value, bool ignoreCase = true) where T : struct
    {
        Check.NotNull(value);

        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    public static string Truncate(this string value, int maxLength)
    {
        if (value is null)
            return null;

        return value.Length <= maxLength
                   ? value
                   : value.Left(maxLength);
    }

    public static string TruncateWithPostfix(this string value, int maxLength, string postfix = "...")
    {
        if (value is null)
            return null;
        if (value.IsNullOrEmpty() || maxLength == 0)
            return string.Empty;
        if (value.Length <= maxLength)
            return value;
        if (maxLength <= postfix.Length)
            return postfix.Left(maxLength);

        return value.Left(maxLength - postfix.Length) + postfix;
    }

    public static string SubstringSafe(this string input, int start, int length)
        => input.Length <= start
               ? ""
               : input.Length - start <= length
                   ? input[start..]
                   : input.Substring(start, length);

    public static string SubstringSafe(this string input, int start)
        => input.Length <= start
               ? ""
               : input[start..];

    public static string ToTitleCase(this string input)
        => input.First().ToString().ToUpper() + input.Substring(1);
}