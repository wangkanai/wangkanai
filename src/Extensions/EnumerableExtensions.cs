// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wangkanai.Detection.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
            => list == null || !list.Any();

        public static bool HasDuplicates<T, TProp>(this IEnumerable<T> list, Func<T, TProp> selector)
            => list.Any(t => !new HashSet<TProp>().Add(selector(t)));
    }
}
