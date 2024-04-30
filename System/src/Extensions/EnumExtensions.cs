// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.ComponentModel;

namespace Wangkanai.Extensions;

public static class EnumExtensions
{
	// [Obsolete]
	// public static string ToStringMistake<T>(this T value)
	// 	where T : Enum
	// 	=> EnumValues<T>.GetNameMistake(value).ToLowerInvariant();

	[DebuggerStepThrough]
	public static string ToOriginalString<T>(this T value)
		where T : Enum
		=> EnumValues<T>.GetNameOriginal(value);

	[DebuggerStepThrough]
	public static string ToLowerString<T>(this T value)
		where T : Enum
		=> EnumValues<T>.GetNameOriginal(value).ToLowerInvariant();

	[DebuggerStepThrough]
	public static string ToUpperString<T>(this T value)
		where T : Enum
		=> EnumValues<T>.GetNameOriginal(value).ToUpperInvariant();

	[DebuggerStepThrough]
	public static bool ContainsOriginal<T>(this string value, T flags)
		where T : Enum
		=> value.ContainSingle(flags) ||
		   flags.GetFlags().Any(item => value.Contains(item.ToOriginalString(), StringComparison.Ordinal));

	[DebuggerStepThrough]
	public static bool ContainsUpper<T>(this string value, T flags)
		where T : Enum
		=> value.ContainSingle(flags) ||
		   flags.GetFlags().Any(item => value.Contains(item.ToUpperString(), StringComparison.Ordinal));

	[DebuggerStepThrough]
	public static bool ContainsLower<T>(this string value, T flags)
		where T : Enum
		=> value.ContainSingle(flags) ||
		   flags.GetFlags().Any(item => value.Contains(item.ToLowerString(), StringComparison.Ordinal));

	[Obsolete]
	public static bool ContainsMistake<T>(this string value, T flags)
		where T : Enum
		=> value.ContainSingleMistake(flags) ||
		   flags.GetFlags().Any(item => value.Contains(item.ToLowerString(), StringComparison.Ordinal));

	[Obsolete]
	private static bool ContainSingleMistake<T>(this string value, T flags)
		where T : Enum
		=> EnumValues<T>.TryGetSingleNameMistake(flags, out var name) &&
		   value.Contains(name, StringComparison.Ordinal);

	private static bool ContainSingle<T>(this string value, T flags)
		where T : Enum
		=> EnumValues<T>.TryGetSingleNameMistake(flags, out var name) &&
		   value.Contains(name, StringComparison.Ordinal);

	[DebuggerStepThrough]
	public static IEnumerable<T> GetFlags<T>(this T value)
		where T : Enum
		=> EnumValues<T>.GetValues()
		                .Where(item => value.HasFlag(item));

	[DebuggerStepThrough]
	public static string GetDescription(this Enum value)
	{
		var type   = value.GetType();
		var member = type.GetMember(value.ToString());
		if (member.Length <= 0)
			return value.ToString();

		var attributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
		if (attributes.IsEmpty())
			return value.ToString();

		return attributes.OfType<DescriptionAttribute>()
		                 .SingleOrDefault()
		                 ?.Description ?? string.Empty;
	}

	[DebuggerStepThrough]
	public static string GetMemberValue(this Enum value)
	{
		var field = value.GetType().GetField(value.ToString());

		if (field is null)
			return string.Empty;

		var attributes = field.GetCustomAttributes(typeof(EnumMemberAttribute), false);
		if (attributes.IsEmpty())
			return value.ToString();

		return attributes.OfType<EnumMemberAttribute>()
		                 .SingleOrDefault()
		                 ?.Value ?? string.Empty;
	}
}
