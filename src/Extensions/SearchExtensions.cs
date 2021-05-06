// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// Modifications Copyright (c) 2021 Kapok Marketing, Inc.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wangkanai.Detection.Extensions
{
    public static class SearchExtensions
    {
        public static IndexTree BuildIndexTree(this string[] keywords) 
            => new IndexTree(keywords);

        public static IndexTree BuildIndexTree(this IEnumerable<string> keywords) 
            => new IndexTree(keywords.Distinct().ToArray());

        public static bool SearchStartsWith(this string searchString, IndexTree searchTree) 
            => searchString.AsSpan().SearchStartsWith(searchTree);

        public static bool SearchStartsWith(this ReadOnlySpan<char> searchString, IndexTree searchTree) 
            => searchTree.StartsWithAnyIn(searchString);

        public static bool SearchContains(this string searchString, IndexTree searchTree) 
            => searchString.AsSpan().SearchContains(searchTree);

        public static bool SearchContains(this ReadOnlySpan<char> searchString, IndexTree searchTree) 
            => searchTree.ContainsWithAnyIn(searchString);
    }
}