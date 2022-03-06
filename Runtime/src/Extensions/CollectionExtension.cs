// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class CollectionExtension
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        => source is null || source.Count <= 0;
}