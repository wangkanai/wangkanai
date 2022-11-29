// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Wangkanai.Extensions;

namespace Wangkanai.Domain.Caching;

public static class CacheKey
{
    public static string With(params string[] keys)
        => string.Join("-", keys);

    public static string With(Type type, params string[] keys)
        => With($"{type.GetCacheKey()}:{string.Join("-", keys)}");
}