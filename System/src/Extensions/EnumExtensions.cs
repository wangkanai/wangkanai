// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.ComponentModel;

namespace Wangkanai.Extensions;

public static class EnumExtensions
{
	[DebuggerStepThrough]
    public static string ToStringInvariant<T>(this T value) where T : Enum
        => EnumValues<T>.GetName(value);

    [DebuggerStepThrough]
    public static bool Contains<T>(this string agent, T flags) where T : Enum
        => EnumValues<T>.TryGetSingleName(flags, out var value) && value != null
               ? agent.Contains(value, StringComparison.Ordinal)
               : flags.GetFlags()
                      .Any(item => agent.Contains(ToStringInvariant(item), StringComparison.Ordinal));

    [DebuggerStepThrough]
    public static IEnumerable<T> GetFlags<T>(this T value) where T : Enum
        => EnumValues<T>.GetValues().Where(item => value.HasFlag(item));

    [DebuggerStepThrough]
    public static string GetDescription(this Enum generic)
    {
        var type   = generic.GetType();
        var member = type.GetMember(generic.ToString());
        if (member.Length <= 0)
            return generic.ToString();

        var attributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attributes.Any())
            return ((DescriptionAttribute)attributes.ElementAt(0)).Description;

        return generic.ToString();
    }
}