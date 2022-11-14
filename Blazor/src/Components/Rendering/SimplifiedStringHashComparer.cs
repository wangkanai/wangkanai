// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.Rendering;

internal sealed class SimplifiedStringHashComparer : IEqualityComparer<string>
{
    public static readonly SimplifiedStringHashComparer Instance = new SimplifiedStringHashComparer();

    public bool Equals(string? x, string? y)
    {
        return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(string key)
    {
        var keyLength = key.Length;
        if (keyLength > 0)
        {
            return unchecked(
                char.ToLowerInvariant(key[keyLength - 1])
                + 31 * char.ToLowerInvariant(key[keyLength / 2])
                + 961 * keyLength);
        }
        else
            return default;
    }
}
