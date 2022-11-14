// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections;

namespace Wangkanai.Blazor.Components.RenderTree;

public readonly struct ArrayBuilderSegment<T> : IEnumerable<T>
{
    private readonly ArrayBuilder<T>? _builder;
    private readonly int              _offset;
    private readonly int              _count;

    internal ArrayBuilderSegment(ArrayBuilder<T> builder, int offset, int count)
    {
        _builder = builder;
        _offset  = offset;
        _count   = count;
    }

    public T[] Array => _builder?.Buffer ?? System.Array.Empty<T>();

    public int Offset => _offset;
    public int Count  => _count;

    /// <summary>
    /// Gets the specified item from the segment.
    /// </summary>
    /// <param name="index">The index into the segment.</param>
    /// <returns>The array entry at the specified index within the segment.</returns>
    public T this[int index]
        => Array[_offset + index];

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => ((IEnumerable<T>)new ArraySegment<T>(Array, _offset, _count)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)new ArraySegment<T>(Array, _offset, _count)).GetEnumerator();
}

