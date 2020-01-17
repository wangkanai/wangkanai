// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Wangkanai.Detection.Extensions
{
    internal static class IEnumerableExtensions
    {
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            if (list is null) return true;
            if (!list.Any()) return true;

            return false;
        }

        [DebuggerStepThrough]
        public static bool HasDuplicates<T, TProp>(this IEnumerable<T> list, Func<T, TProp> selector)
        {
            var duplicate = new HashSet<TProp>();
            foreach (var t in list)
                if (!duplicate.Add(selector(t))) return true;

            return false;
        }
    }
}
