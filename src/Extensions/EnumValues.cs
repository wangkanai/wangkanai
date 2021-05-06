// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// Modifications Copyright (c) 2021 Kapok Marketing, Inc.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wangkanai.Detection.Extensions
{
    public static class EnumValues<T>
        where T : Enum
    {
        public static readonly T[] Values;

        private static readonly Dictionary<T, string> Names = new Dictionary<T, string>();

        static EnumValues()
        {
            Values = (T[]) Enum.GetValues(typeof(T));
            foreach (var value in Values) 
                Names.Add(value, value.ToString().ToLowerInvariant());
        }

        public static string GetName(T value) 
            => Names.TryGetValue(value, out var result) 
                   ? result 
                   : string.Join(',', value.GetFlags().Select(x => Names[x]));

        public static bool TryGetSingleName(T value, out string? result) 
            => Names.TryGetValue(value, out result);
    }
}