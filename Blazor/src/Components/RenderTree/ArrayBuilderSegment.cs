// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections;

namespace Wangkanai.Blazor.Components.RenderTree;

public readonly struct ArrayBuilderSegment<T> : IEnumerable<T>
{
    private readonly ArrayBuilder<T>? _builder;

    internal ArrayBuilderSegment(ArrayBuilder<T> builder, int offset, int count)
    {
        _builder = builder;
        Offset   = offset;
        Count    = count;
    }

    public T[] Array => _builder?.Buffer ?? System.Array.Empty<T>();

    public int Offset { get; }

    public int Count { get; }

    /// <summary>
    ///     Gets the specified item from the segment.
    /// </summary>
    /// <param name="index">The index into the segment.</param>
    /// <returns>The array entry at the specified index within the segment.</returns>
    public T this[int index]
        => Array[Offset + index];

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ((IEnumerable<T>)new ArraySegment<T>(Array, Offset, Count)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)new ArraySegment<T>(Array, Offset, Count)).GetEnumerator();
    }
}