// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public sealed class RandomAccessQueue<T> : ICollection<T>, ICollection, ICloneable
{
	public const int DefaultCapacity = 16;

	private int _version = 0;

	private T[] _buffer;
	private int _count;
	private int _start;

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

	public RandomAccessQueue() : this(DefaultCapacity) { }

	public RandomAccessQueue(int capacity)
		=> _buffer = new T[System.Math.Max(capacity, DefaultCapacity)];

	private RandomAccessQueue(T[] buffer, int count, int start)
	{
		_buffer = (T[])buffer.Clone();
		_count  = count;
		_start  = start;
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

	/// <summary>
	/// Clear the queue without resizing the buffer
	/// </summary>
	public void Clear()
	{
		_start = 0;
		_count = 0;
		((IList)_buffer).Clear();
	}

	/// <summary>
	/// Strongly type version if <see cref="ICloneable.Clone"/>
	/// Create a new queue with the same contents as the queue.
	/// </summary>
	/// <returns>A clone of the current queue</returns>
	public object Clone()
		=> new RandomAccessQueue<T>(_buffer, _count, _start);

	public void Add(T item)
	{
		Enquene(item);
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

	public void Enquene(T value)
		=> Enqueue(value, _count);

	public void Enqueue(T value, int index)
	{
		if (_count == Capacity)
		{
			Resize(_count * 2, index);
			_count++;
		}
		else
		{
			_count++;
			// How to make this more efficient? foreach?
			for (int i = _count - 2; i >= index; i--)
				this[i + 1] = this[i];
		}

		this[index] = value;
	}

	public T Dequeue()
	{
		if (_count == 0)
			throw new InvalidOperationException("Dequeue called from an empty queue");

		var result = this[0];
		this[0] = default;
		_start++;
		if (_start == Capacity)
			_start = 0;

		_count--;
		return result;
	}

	private void Resize(int newCapacity, int gap) { }

	public IEnumerator<T> GetEnumerator()
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}