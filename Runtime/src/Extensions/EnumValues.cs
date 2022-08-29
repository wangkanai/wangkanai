// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class EnumValues<T> where T : Enum
{
    private static readonly Dictionary<T, string> Names = new();
    private static readonly T[] Values;

    static EnumValues()
    {
        Values = (T[])Enum.GetValues(typeof(T));
        foreach (var value in Values)
            Names.Add(value, value.ToString().ToLowerInvariant());
    }

    public static T[] GetValues()
        => Values;

    public static Dictionary<T, string> GetNames()
        => Names;

    public static string GetName(T value)
    {
        var flags = value.GetFlags().Select(x => Names[x]);
        return Names.TryGetValue(value, out var result)
                   ? result
                   : string.Join(',', flags);
    }

    public static bool TryGetSingleName(T value, out string result)
        => Names.TryGetValue(value, out result);
}