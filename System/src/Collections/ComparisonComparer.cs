// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public sealed class ComparisonComparer<T> : IComparer<T>
{
    private readonly Comparison<T> comparison;

    public ComparisonComparer(Comparison<T> comparison)
    {
        comparison.IfNullThrow();
        this.comparison = comparison;
    }

    public int Compare(T x, T y)
        => comparison(x, y);

    public static Comparison<T> CreateComparison(IComparer<T> comparer)
    {
        comparer.IfNullThrow();
        return delegate(T x, T y) { return comparer.Compare(x, y); };
    }
}