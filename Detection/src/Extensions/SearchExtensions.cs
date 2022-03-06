// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Extensions;

public static class SearchExtensions
{
    public static IndexTree BuildIndexTree(this string[] keywords)
        => new(keywords);

    public static IndexTree BuildIndexTree(this IEnumerable<string> keywords)
        => new(keywords.Distinct().ToArray());

    public static bool SearchStartsWith(this string searchString, IndexTree searchTree)
        => searchString.AsSpan().SearchStartsWith(searchTree);

    private static bool SearchStartsWith(this ReadOnlySpan<char> searchString, IndexTree searchTree)
        => searchTree.StartsWithAnyIn(searchString);

    public static bool SearchContains(this string searchString, IndexTree searchTree)
        => searchString.AsSpan().SearchContains(searchTree);

    public static bool SearchContains(this ReadOnlySpan<char> searchString, IndexTree searchTree)
        => searchTree.ContainsWithAnyIn(searchString);
}