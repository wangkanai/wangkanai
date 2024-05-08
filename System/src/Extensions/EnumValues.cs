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
	private static T[] Values => (T[])Enum.GetValues(typeof(T));

	private static Dictionary<T, string> NamesOriginal => Values.ToDictionary(value => value, value => value.ToString());
	private static Dictionary<T, string> NamesLower    => Values.ToDictionary(value => value, value => value.ToString().ToLowerInvariant());
	private static Dictionary<T, string> NamesUpper    => Values.ToDictionary(value => value, value => value.ToString().ToUpperInvariant());

	/// <summary>
	/// Retrieves an array of all values of the specified enum type.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <returns>An array of all values of the specified enum type.</returns>
	public static T[] GetValues()
		=> Values;

	/// <summary>
	/// Retrieves a dictionary of all names corresponding to the values of the specified enum type.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <returns>A dictionary of all names corresponding to the values of the specified enum type.</returns>
	public static Dictionary<T, string> GetNames()
		=> NamesOriginal;

	/// <summary>
	/// Tries to retrieve the single name of the specified enumeration value.
	/// </summary>
	/// <typeparam name="T">The type of the enumeration.</typeparam>
	/// <param name="value">The enumeration value to get the name of.</param>
	/// <param name="result">When this method returns, contains the single name associated with the specified enumeration value, if the retrieval succeeds; otherwise, an empty string. This parameter is passed uninitialized.</param>
	/// <returns><see langword="true"/> if the single name of the specified enumeration value is successfully retrieved; otherwise <see langword="false"/>.</returns>
	public static bool TryGetSingleName(T value, out string result)
		=> NamesOriginal.TryGetValue(value, out result!);

	/// <summary>
	/// Retrieves the original name(s) of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value to retrieve the original name(s) for.</param>
	/// <returns>The original name(s) of the specified enum value.</returns>
	public static string GetNameOriginal(T value)
		=> value.GetFlags().Any()
			   ? string.Join(',', value.GetFlags().Select(x => NamesOriginal[x]))
			   : NamesOriginal[value];

	/// <summary>
	/// Retrieves the lowercased name of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value.</param>
	/// <returns>The lowercased name of the enum value.</returns>
	public static string GetNameLower(T value)
		=> value.GetFlags().Any()
			   ? string.Join(',', value.GetFlags().Select(x => NamesLower[x]))
			   : NamesLower[value];

	/// <summary>
	/// Retrieves the uppercased name of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value.</param>
	/// <returns>The uppercased name of the specified enum value.</returns>
	public static string GetNameUpper(T value)
		=> value.GetFlags().Any()
			   ? string.Join(',', value.GetFlags().Select(x => NamesUpper[x]))
			   : NamesUpper[value];
}
