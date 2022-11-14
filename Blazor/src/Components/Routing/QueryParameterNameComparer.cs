// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

namespace Wangkanai.Blazor.Components.Routing;

internal sealed class QueryParameterNameComparer : IComparer<ReadOnlyMemory<char>>, IEqualityComparer<ReadOnlyMemory<char>>
{
    public static readonly QueryParameterNameComparer Instance = new();

    public int Compare(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        => x.Span.CompareTo(y.Span, StringComparison.OrdinalIgnoreCase);

    public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        => x.Span.Equals(y.Span, StringComparison.OrdinalIgnoreCase);

    public int GetHashCode([DisallowNull] ReadOnlyMemory<char> obj)
        => string.GetHashCode(obj.Span, StringComparison.OrdinalIgnoreCase);
}