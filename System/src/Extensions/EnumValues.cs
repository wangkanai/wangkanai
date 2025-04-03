// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

/// <summary>
/// Provides utility methods for working with enum values.
/// </summary>
/// <typeparam name="T">The type of the enum.</typeparam>
/// <remarks>
/// This class is designed to work with enums that have the [Flags] attribute.
/// </remarks>
[DebuggerStepThrough]
public static class EnumValues<T> where T : Enum
{
	private static readonly T[]                Values    = (T[])Enum.GetValues(typeof(T));
	private static readonly Dictionary<T, T[]> EnumFlags = Values.ToDictionary(v => v, v => Values.Where(item => v.HasFlag(item)).ToArray());

	private static readonly Dictionary<string, T>    ValuesOriginal   = Values.ToDictionary(value => value.ToString(), value => value, StringComparer.Ordinal);
	private static readonly Dictionary<string, T>    ValuesIgnoreCase = Values.ToDictionary(value => value.ToString(), value => value, StringComparer.OrdinalIgnoreCase);

	private static readonly Dictionary<T, EnumValueName> NamesOriginal
		= Values.ToDictionary(value => value, value => new EnumValueName
		                                               {
			                                               Name          = value.ToString(),
			                                               NameWithFlags = GetNameWithFlags(value)
		                                               });
	private static readonly Dictionary<T, EnumValueName> NamesLower
		= Values.ToDictionary(value => value, value => new EnumValueName
		                                               {
			                                               Name          = value.ToString().ToLowerInvariant(),
			                                               NameWithFlags = GetNameWithFlags(value).ToLowerInvariant()
		                                               });

	private static readonly Dictionary<T, EnumValueName> NamesUpper
		= Values.ToDictionary(value => value, value => new EnumValueName
		                                               {
			                                               Name          = value.ToString().ToUpperInvariant(),
			                                               NameWithFlags = GetNameWithFlags(value).ToUpperInvariant()
		                                               });

	private static string GetNameWithFlags(T value)
	{
		return value.GetFlags().Any()
			       ? string.Join(',', value.GetFlags().Select(x => x.ToString()))
			       : value.ToString();
	}

	/// <summary>
	/// Retrieves an array of all values of the specified enum type.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <returns>An array of all values of the specified enum type.</returns>
	public static T[] GetValues()
		=> Values;

	public static T[] GetFlags(T value)
	{
		return EnumFlags[value];
	}

	/// <summary>
	/// Retrieves a dictionary of all names corresponding to the values of the specified enum type.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <returns>A dictionary of all names corresponding to the values of the specified enum type.</returns>
	public static Dictionary<T, EnumValueName> GetNames()
		=> NamesOriginal;

	/// <summary>
	/// Tries to retrieve the single name of the specified enumeration value.
	/// </summary>
	/// <typeparam name="T">The type of the enumeration.</typeparam>
	/// <param name="value">The enumeration value to get the name of.</param>
	/// <param name="result">When this method returns, contains the single name associated with the specified enumeration value, if the retrieval succeeds; otherwise, an empty string. This parameter is passed uninitialized.</param>
	/// <returns><see langword="true"/> if the single name of the specified enumeration value is successfully retrieved; otherwise <see langword="false"/>.</returns>
	public static bool TryGetSingleName(T value, [MaybeNullWhen(false)] out string result)
	{
		var exist = NamesOriginal.TryGetValue(value, out var names);
		if (exist)
		{
			result = names.Name;
			return true;
		}

		result = null;
		return false;
	}

	/// <summary>
	/// Retrieves the original name(s) of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value to retrieve the original name(s) for.</param>
	/// <returns>The original name(s) of the specified enum value.</returns>
	public static string GetNameOriginal(T value) => NamesOriginal[value].NameWithFlags;

	/// <summary>
	/// Retrieves the lowercased name of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value.</param>
	/// <returns>The lowercased name of the enum value.</returns>
	public static string GetNameLower(T value)
		=> NamesLower[value].NameWithFlags;

	/// <summary>
	/// Retrieves the uppercased name of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value.</param>
	/// <returns>The uppercased name of the specified enum value.</returns>
	public static string GetNameUpper(T value)
		=> NamesUpper[value].NameWithFlags;

	public static T Parse(string valueName, bool ignoreCase)
	{
		if (ignoreCase)
		{
			return ValuesIgnoreCase[valueName];
		}
		else
		{
			return ValuesOriginal[valueName];
		}
	}
}

public readonly struct EnumValueName
{
	public string Name          { get; init; }
	public string NameWithFlags { get; init; }
}
