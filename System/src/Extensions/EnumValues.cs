// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class EnumValues<T> where T : Enum
{
	[DebuggerStepThrough]
	public static T[] GetValues()
		=> Values;

	[DebuggerStepThrough]
	public static Dictionary<T, string> GetNames()
		=> NamesLower;

	[DebuggerStepThrough]
	public static string GetName(T value)
		=> value.GetFlags().Any()
			   ? string.Join(',', value.GetFlags().Select(x => Names[x]))
			   : Names[value];

	internal static bool TryGetSingleName(T value, out string result)
		=> NamesLower.TryGetValue(value, out result!);

	private static T[] Values
		=> (T[])Enum.GetValues(typeof(T));

	private static Dictionary<T, string> Names
		=> Values.ToDictionary(value => value, value => value.ToString());

	private static Dictionary<T, string> NamesUpper
		=> Values.ToDictionary(value => value, value => value.ToString().ToUpperInvariant());

	private static Dictionary<T, string> NamesLower
		=> Values.ToDictionary(value => value, value => value.ToString().ToLowerInvariant());
}
