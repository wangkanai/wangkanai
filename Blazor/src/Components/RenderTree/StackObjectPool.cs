// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.RenderTree;

internal sealed class StackObjectPool<T> where T : class
{
    private readonly T[]     _contents;
    private readonly Func<T> _instanceFactory;
    private readonly int     _maxPreservedItems;
    private          int     _numSuppliedItems;
    private          int     _numTrackedItems;

    public StackObjectPool(int maxPreservedItems, Func<T> instanceFactory)
    {
        _maxPreservedItems = maxPreservedItems;
        _instanceFactory   = instanceFactory ?? throw new ArgumentNullException(nameof(instanceFactory));
        _contents          = new T[_maxPreservedItems];
    }

    public T Get()
    {
        _numSuppliedItems++;

        if (_numSuppliedItems <= _maxPreservedItems)
        {
            if (_numTrackedItems < _numSuppliedItems)
            {
                // Need to allocate a new one
                var newItem = _instanceFactory();
                _contents[_numTrackedItems++] = newItem;
                return newItem;
            }

            // Can use one that's already in the pool
            return _contents[_numSuppliedItems - 1];
        }

        // Pool is full; return untracked instance
        return _instanceFactory();
    }

    public void Return(T instance)
    {
        if (_numSuppliedItems <= 0) throw new InvalidOperationException("There are no outstanding instances to return.");

        if (_numSuppliedItems <= _maxPreservedItems)
        {
            // We check you're returning the right instance only as a way of
            // catching Get/Return mismatch bugs
            var expectedInstance = _contents[_numSuppliedItems - 1];
            if (!ReferenceEquals(instance, expectedInstance)) throw new ArgumentException($"Attempting to return wrong pooled instance. {nameof(Get)}/{nameof(Return)} calls must form a stack.");
        }

        // It's a valid call. Track that we're no longer "supplying" the top item,
        // but keep the instance in the _contents array for future reuse.
        _numSuppliedItems--;
    }
}