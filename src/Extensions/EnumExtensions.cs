// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wangkanai.Detection.Extensions
{
    internal static class EnumExtensions
    {
        public static bool Contains<T>(this string agent, T flags)
            where T : Enum
            => EnumValues<T>.TryGetSingleName(flags, out var value) && value != null 
                   ? agent.Contains(value, StringComparison.Ordinal) 
                   : flags.GetFlags().Any(item => agent.Contains(item.ToStringInvariant(), StringComparison.Ordinal));

        public static string ToStringInvariant<T>(this T value)
            where T : Enum
            => EnumValues<T>.GetName(value);

        public static IEnumerable<T> GetFlags<T>(this T value)
            where T : Enum
        {
            var values = EnumValues<T>.Values;

            foreach (var item in values)
                if (value.HasFlag(item))
                    yield return item;
        }
    }
}