// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class EnumValues<T> where T : Enum
{
	[DebuggerStepThrough]
	public static T[] GetValues()
		=> Values;

	[DebuggerStepThrough]
	public static Dictionary<T, string> GetNames()
		=> NamesOriginal;

	[DebuggerStepThrough]
	public static bool TryGetSingleName(T value, out string result)
		=> NamesOriginal.TryGetValue(value, out result!);

	[DebuggerStepThrough]
	public static string GetNameOriginal(T value)
		=> value.GetFlags().Any()
			   ? string.Join(',', value.GetFlags().Select(x => NamesOriginal[x]))
			   : NamesOriginal[value];

	private static T[] Values
		=> (T[])Enum.GetValues(typeof(T));

	private static Dictionary<T, string> NamesMistake
		=> Values.ToDictionary(value => value, value => value.ToString().ToLowerInvariant());

	private static Dictionary<T, string> NamesLower
		=> Values.ToDictionary(value => value, value => value.ToString().ToLowerInvariant());

	private static Dictionary<T, string> NamesUpper
		=> Values.ToDictionary(value => value, value => value.ToString().ToUpperInvariant());

	private static Dictionary<T, string> NamesOriginal
		=> Values.ToDictionary(value => value, value => value.ToString());
}
