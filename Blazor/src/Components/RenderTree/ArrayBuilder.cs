// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Wangkanai.Blazor.Components.RenderTree;

#pragma warning disable CA1852 // Seal internal types
internal class ArrayBuilder<T> : IDisposable
#pragma warning restore CA1852 // Seal internal types
{
    protected T[] _items;
    protected int _itemsInUse;

    private static readonly T[]          Empty = Array.Empty<T>();
    private readonly        ArrayPool<T> _arrayPool;
    private readonly        int          _minCapacity;
    private                 bool         _disposed;
    
    public ArrayBuilder(int minCapacity = 32, ArrayPool<T> arrayPool = null)
    {
        _arrayPool = arrayPool ?? ArrayPool<T>.Shared;
        _minCapacity = minCapacity;
        _items = Empty;
    }
    
    public int Count => _itemsInUse;
    
    public T[] Buffer => _items;

    /// <summary>
    /// Appends a new item, automatically resizing the underlying array if necessary.
    /// </summary>
    /// <param name="item">The item to append.</param>
    /// <returns>The index of the appended item.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] // Just like System.Collections.Generic.List<T>
    public int Append(in T item)
    {
        if (_itemsInUse == _items.Length)
        {
            GrowBuffer(_items.Length * 2);
        }

        var indexOfAppendedItem = _itemsInUse++;
        _items[indexOfAppendedItem] = item;
        return indexOfAppendedItem;
    }

    internal int Append(T[] source, int startIndex, int length)
        => Append(source.AsSpan(startIndex, length));

    internal int Append(ReadOnlySpan<T> source)
    {
        // Expand storage if needed. Using same doubling approach as would
        // be used if you inserted the items one-by-one.
        var requiredCapacity = _itemsInUse + source.Length;
        if (_items.Length < requiredCapacity)
        {
            var candidateCapacity = Math.Max(_items.Length * 2, _minCapacity);
            while (candidateCapacity < requiredCapacity)
            {
                candidateCapacity *= 2;
            }

            GrowBuffer(candidateCapacity);
        }

        source.CopyTo(_items.AsSpan(_itemsInUse));
        var startIndexOfAppendedItems = _itemsInUse;
        _itemsInUse += source.Length;
        return startIndexOfAppendedItems;
    }

    /// <summary>
    /// Sets the supplied value at the specified index. The index must be within
    /// range for the array.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Overwrite(int index, in T value)
    {
        if (index > _itemsInUse)
        {
            ThrowIndexOutOfBoundsException();
        }

        _items[index] = value;
    }
    
    public void RemoveLast()
    {
        if (_itemsInUse == 0)
        {
            ThrowIndexOutOfBoundsException();
        }

        _itemsInUse--;
        _items[_itemsInUse] = default; // Release to GC
    }
    
    public void InsertExpensive(int index, T value)
    {
        if (index > _itemsInUse)
        {
            ThrowIndexOutOfBoundsException();
        }

        if (_itemsInUse == _items.Length)
        {
            GrowBuffer(_items.Length * 2);
        }

        Array.Copy(_items, index, _items, index + 1, _itemsInUse - index);
        _itemsInUse++;

        _items[index] = value;
    }
    
    public void Clear()
    {
        ReturnBuffer();
        _items = Empty;
        _itemsInUse = 0;
    }

    protected void GrowBuffer(int desiredCapacity)
    {
        ObjectDisposedException.ThrowIf(_disposed, null);

        var newCapacity = Math.Max(desiredCapacity, _minCapacity);
        Debug.Assert(newCapacity > _items.Length);

        var newItems = _arrayPool.Rent(newCapacity);
        Array.Copy(_items, newItems, _itemsInUse);
        
        ReturnBuffer();
        _items = newItems;
    }

    private void ReturnBuffer()
    {
        if (!ReferenceEquals(_items, Empty))
        {
            Array.Clear(_items, 0, _itemsInUse);
            _arrayPool.Return(_items);
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            ReturnBuffer();
            _items = Empty;
            _itemsInUse = 0;
        }
    }

    private static void ThrowIndexOutOfBoundsException()
    {
        throw new ArgumentOutOfRangeException("index");
    }
}
