// Copyright (c) 2014-2024 Sarin Na Wangkanai and Aliaksandr Kukrash, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;

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
	private static readonly T[] Values = (T[])Enum.GetValues(typeof(T));

	private static readonly ConcurrentDictionary<T, IReadOnlySet<T>> MultiValueFlagsCache = new();
	private static readonly ConcurrentDictionary<T, EnumValueName> MultiValueCache = new();

	private static readonly Dictionary<T, string> PlainNames = Values.ToDictionary(value => value, value => value.ToString());
	private static readonly Dictionary<T, IReadOnlySet<T>> EnumFlags = Values.ToDictionary(v => v, v => (IReadOnlySet<T>)Values.Where(item => v.HasFlag(item)).ToHashSet());
	private static readonly Dictionary<string, T> ValuesOriginal = Values.ToDictionary(value => value.ToString(), value => value, StringComparer.Ordinal);
	private static readonly Dictionary<string, T> ValuesIgnoreCase = Values.ToDictionary(value => value.ToString(), value => value, StringComparer.OrdinalIgnoreCase);

	/// <summary>
	/// Retrieves an array of all values of the specified enum type.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <returns>An array of all values of the specified enum type.</returns>
	public static T[] GetValues()
		=> Values;

	/// <summary>
	/// Retrieves the individual flags that are set for the specified enum value.
	/// </summary>
	/// <param name="value">The enumeration value to extract the flags from.</param>
	/// <returns>A read-only set containing the flags that are part of the specified enumeration value.</returns>
	public static IReadOnlySet<T> GetFlags(T value)
		=> EnumFlags.TryGetValue(value, out var flags)
			   ? flags
			   : MultiValueFlagsCache.GetOrAdd(value, v => Values.Where(item => v.HasFlag(item)).ToHashSet());

	/// <summary>
	/// Retrieves all names corresponding to the values of the specified enum type.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <returns>All names corresponding to the values of the specified enum type.</returns>
	public static IEnumerable<string> GetNames()
		=> Values.Select(v => GetNameWithFlagsCached(v, false, ValueKind.Normal));

	/// <summary>
	/// Tries to retrieve the single name of the specified enumeration value.
	/// </summary>
	/// <typeparam name="T">The type of the enumeration.</typeparam>
	/// <param name="value">The enumeration value to get the name of.</param>
	/// <param name="result">When this method returns, contains the single name associated with the specified enumeration value, if the retrieval succeeds; otherwise, an empty string. This parameter is passed uninitialized.</param>
	/// <returns><see langword="true"/> if the single name of the specified enumeration value is successfully retrieved; otherwise <see langword="false"/>.</returns>
	public static bool TryGetSingleName(T value, [MaybeNullWhen(false)] out string result)
		=> PlainNames.TryGetValue(value, out result);

	/// <summary>
	/// Retrieves the original name(s) of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value to retrieve the original name(s) for.</param>
	/// <returns>The original name(s) of the specified enum value.</returns>
	public static string GetNameOriginal(T value) =>
		GetNameWithFlagsCached(value, true, ValueKind.Normal);

	/// <summary>
	/// Retrieves the lowercased name of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value.</param>
	/// <returns>The lowercased name of the enum value.</returns>
	public static string GetNameLower(T value)
		=> GetNameWithFlagsCached(value, true, ValueKind.Lower);

	/// <summary>
	/// Retrieves the uppercased name of the specified enum value.
	/// </summary>
	/// <typeparam name="T">The type of the enum.</typeparam>
	/// <param name="value">The enum value.</param>
	/// <returns>The uppercased name of the specified enum value.</returns>
	public static string GetNameUpper(T value)
		=> GetNameWithFlagsCached(value, true, ValueKind.Upper);

	/// <summary>
	/// Returns corresponding enum value based on its string representation
	/// </summary>
	/// <param name="valueName"></param>
	/// <param name="ignoreCase"></param>
	/// <returns>Enum value</returns>
	/// <exception cref="ArgumentException"></exception>
	public static T Parse(string valueName, bool ignoreCase)
	{
		if (ignoreCase)
		{
			if (ValuesIgnoreCase.TryGetValue(valueName, out var result))
				return result;
		}
		else
		{
			if (ValuesOriginal.TryGetValue(valueName, out var result))
				return result;
		}

		throw new ArgumentException($"The value '{valueName}' is not recognized.");
	}

	private static string GetNameWithFlags(T value)
		=> value.GetFlags().Any()
			   ? string.Join(',', value.GetFlags().Select(x => x.ToString()))
			   : value.ToString();

	private static string GetNameWithFlagsCached(T value, bool returnAllFlags, ValueKind kind)
	{
		var names = MultiValueCache
			.GetOrAdd(value, v => new EnumValueName
			{
				Name = v.ToString(),
				NameWithFlags = GetNameWithFlags(v),
				NameLower = v.ToString().ToLowerInvariant(),
				NameLowerWithFlags = GetNameWithFlags(v).ToLowerInvariant(),
				NameUpper = v.ToString().ToUpperInvariant(),
				NameUpperWithFlags = GetNameWithFlags(v).ToUpperInvariant()
			});

		if (returnAllFlags)
			return kind switch
			{
				ValueKind.Normal => names.NameWithFlags,
				ValueKind.Lower => names.NameLowerWithFlags,
				ValueKind.Upper => names.NameUpperWithFlags,
				_ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
			};

		return kind switch
		{
			ValueKind.Normal => names.Name,
			ValueKind.Lower => names.NameLower,
			ValueKind.Upper => names.NameUpper,
			_ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
		};
	}

	private enum ValueKind
	{
		Normal = 0,
		Lower = 1,
		Upper = 2,
	}

	private readonly struct EnumValueName
	{
		public required string Name { get; init; }
		public required string NameWithFlags { get; init; }
		public required string NameLower { get; init; }
		public required string NameLowerWithFlags { get; init; }
		public required string NameUpper { get; init; }
		public required string NameUpperWithFlags { get; init; }
	}
}
