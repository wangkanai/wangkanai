// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public sealed class RandomAccessQueue<T> : ICollection<T>, ICollection, ICloneable
{
	public const int DefaultCapacity = 16;

	private T[] _buffer;
	private int _count;
	private int _start;
	private int _version = 0;

	public int Count    => _count;
	public int Capacity => _buffer.Length;

	public T this[int index]
	{
		get
		{
			index.ThrowIfOutOfRange(0, _count);

			return _buffer[(_start + index) % Capacity];
		}
		set
		{
			index.ThrowIfOutOfRange(0, _count);
			
			_version++;
			_buffer[(_start + index) % Capacity] = value;
		}
	}


	public RandomAccessQueue(int capacity = DefaultCapacity)
	{
		_buffer = new T[System.Math.Max(capacity, DefaultCapacity)];
	}

	public bool IsReadOnly
	{
		get;
	}

	public bool IsSynchronized
	{
		get;
	}

	public object SyncRoot
	{
		get;
	}

	public IEnumerator<T> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Add(T item)
	{
		throw new NotImplementedException();
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public bool Contains(T item)
	{
		throw new NotImplementedException();
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		throw new NotImplementedException();
	}

	public bool Remove(T item)
	{
		throw new NotImplementedException();
	}

	public void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}


	public object Clone()
	{
		throw new NotImplementedException();
	}
}