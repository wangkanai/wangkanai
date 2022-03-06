// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        => list is null || !list.Any();

    public static bool HasDuplicates<T, TProp>(this IEnumerable<T> list, Func<T, TProp> selector)
        => list.Any(t => !new HashSet<TProp>().Add(selector(t)));
}