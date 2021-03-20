using System;
using System.Collections.Generic;
using System.Linq;

namespace Wangkanai.Detection.Extensions
{
    public static class EnumValues<T>
        where T : Enum
    {
        public static readonly T[] Values;

        private static readonly Dictionary<T, string> Names = new();

        static EnumValues()
        {
            Values = (T[]) Enum.GetValues(typeof(T));
            foreach (var value in Values)
            {
                Names.Add(value, value.ToString().ToLowerInvariant());
            }
        }

        public static string GetName(T value)
        {
            if (Names.TryGetValue(value, out var result))
            {
                return result;
            }

            return string.Join(',', value.GetFlags().Select(x => Names[x]));
        }

        public static bool TryGetSingleName(T value, out string? result)
        {
            return Names.TryGetValue(value, out result);
        }
    }
}